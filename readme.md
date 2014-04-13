# <a id="top"></a>RezRouting.Net

A library that sets up ASP.Net MVC routes using resource-oriented URLs - similar to Ruby on Rails' built-in [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default):

- Simple configuration API, convention-based naming and smart action discovery - map a resource's routes in one line of code
- Clean URLs, with all the formatting options you need (lowercase, uppercase (surely not!), dashed, underscored)
- Support for singular resources, collections and various nested combinations, e.g. /photos/27229/comments
- Partition actions for a resource into separate controllers - no more bloated controllers and SRP violations

## Contents
 - [Introduction](#intro)
 - [Background - Thinking Resourcefully](#background)
 - [Getting Started](#getting-started)
 - [Mapping the "/" home route](#home-route)
 - [Common Resource Configuration)(#common-resource-configuration)
 - [Mapping Collections](#collections)
 - [Singular Resource Routes](#singular)
 - [Adding Custom Routes](#singular)
 - [General Configuration](#configuration)
 - [Author](#author)
 - [License](#license)

## <a id="intro"></a>Introduction

RezRouting.Net provides a simple API for setting up resource-oriented routes in an ASP.Net MVC web application. Here's some example code that you'd run at startup to map routes for an /albums collection resource, which, in turn contains a nested photos collection:

```C#
var builder = new RootResourceBuilder();
builder.Collection(albums =>
{
  albums.HandledBy<AlbumsController>();
  albums.Collection(photos => photos.HandledBy<PhotosController>());
});
// Add to the application's RouteCollection
builder.MapRoutes(RouteTable.Routes);
```
This would map several routes, linking web requests (based on HTTP method and path) to the controller actions. Selected examples (not all) of the routes created are listed below:

- `GET /albums` displays a list of albums - AlbumsController.Index action
- `GET /albums/123` display an individual album - AlbumsController.Show action
- `POST /albums` creates a new album - AlbumsController.Create action
- `GET /albums/123/photos` displays photos in an individual album - PhotosController.Show action
- `DELETE /albums/123/photos/456` removes photo 456 from album 123 - PhotosController.Delete action

Setting up routes like this manually is tedious and error-prone. Let RezRouting do the work for you.

RezRouting was inspired by [Ruby on Rails RESTful Routing](http://guides.rubyonrails.org/routing.htmlrouting) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). RezRouting's default routes are structured in the same way as those created by Ruby on Rails out-of-the box. See [Background - Thinking Resourcefully](#background) below for further background on this type of URL structure or skip ahead to [getting started](#getting-started)

## <a id="background"></a>Background - Thinking Resourcefully

This section is intended for users new to the concept of REST, resources and routes - you can skim this section or skip ahead to [getting started](#getting-started) if you already _get_ resourceful URLs (ha ha - PRs to remove dubious humour always accepted).

Forget the theoretical discussions about [REST](http://en.wikipedia.org/wiki/Representational_state_transfer) for now. From a purely practical point of view, structuring your URLs and controllers / actions in terms of your application's resources and the actions available on them fits a surprisingly wide range of scenarios. Using an established convention leaves you to get on with important stuff - it's the default routing setup that comes with Ruby on Rails. Also, the URLs are quite pretty and easy to follow.

Put simply, a resource is a "thing" within your web application, a product, a photo album, a photo etc. A resource focussed approach to structuring your application involves uses existing web architecture and conventions to allow users to view and perform actions on these things.

- Firstly, an application's resources are accessed using logically structured URL paths that represent the resources and the relationships between them. A URL like "/users/phoebe/photos" is resourceful. URLs like "scripts/photos.php?uid=2777&pid=36888" or "index.cfm?fuseaction=photos..." are not...

- Secondly, the actions that a user can perform upon these resources are based around HTTP methods (again we're using an existing web mechanisms). HTTP methods are based on the web's document oriented roots, but they work quite well for conceptual resources too! `GET /users/phoebe/photos` displays an HTML page containing a user's photos, `POST /users/phoebe/photos` adds a new photo, `PUT /users/phoebe/photos/123` updates a specific photo.

So, how are resourceful URLs structured?

Imagine a simple web application used to maintain a list of photos. A resource-oriented URL / controller structure would display photo information using the URL _/photos_, routing requests to actions on a _PhotosController_ controller. Different URLs and HTTP methods would be used to access different controller actions. The URLs for the common CRUD (create, read, update and delete) actions and the way in which they relate to controller actions are displayed below.

|HTTP Verb|URL Path           |Controller#Action|Purpose|
|---------|------------------ |-----------------|-------------|
|GET      |/photos            |photos#index     |Display a list of all photos |
|GET	  |/photos/new        |photos#new       |Display form for creating a new item |
|POST	  |/photos	      |photos#create    |Create a new photo |
|GET	  |/photos/{id}	      |photos#show	|Display a specific photo |
|GET	  |/photos/{id}/edit  |photos#edit	|Display for for editing specific item |
|PUT	  |/photos/{id}	      |photos#update	|update a specific photo |
|DELETE	  |/photos/{id}	      |photos#destroy	|delete a specific photo|

Don't be put off too much by the restrictions that this CRUD structure might enforce. A complex system can still be modelled around resourceful URLs.

Resources come in 2 types, based on plurality:

- A _singular_ resource refers to a single "thing" or entity in your application
- A _collection_ resource contains multiple "things" or entities. Both the collection itself and the items within it are resources in their own right.

Often, resources are structured within others. If our simple photo application was structured around photo albums, we might make an albums resource available at _/albums_, with photos nested beneath.

GET /albums/123 - show details of album with ID 123
GET /albums/123/photos - show list of photos in album 123
GET /albums/123/photos/new - add details of a new photo to add to album 123

The same structure works well in other scenarios. A sequence of screens used to input details of an order into a CRM might be structured as follows:

GET orders/new - screen to start entering details of a new order
POST orders - save new order
GET orders/123/customer/edit - edit the customer details for order (which has an id of 123)
GET orders/123/shipping/edit - edit the shipping address
PUT orders/123/shipping/edit - save the shipping address
GET orders/123/payments/new - enter details of a new payment to record against the order

## <a id="getting-started"></a>Getting Started

RezRouting is available on [nuget](https://nuget.org/packages/RezRouting):

`Install-Package RezRouting`

Once you have RezRouting referenced, you need to configure your routes at application start-up. You might include your configuration code in one of the following places:

- A static RouteConfig.Init method in App_Start\RouteConfig.cs file that you call from Global.as
- Directly within the Application_Start method in Global.asax (might be OK for a small app)
- In a larger application, you might have separate parts of your application set up their own routes separately. You can incorporate RezRouting easily into any existing start-up mechanism, each section just needs to use code similar to that outlined below to add its routes.

Routes are initialised as follows:

```C#
var root = new RootResourceBuilder();
root.Collection(albums =>
{
  albums.HandledBy<AlbumsController>();
  albums.Collection(photos => photos.HandledBy<PhotosController>());
});
// Add to the application's RouteCollection
root.MapRoutes(RouteTable.Routes);
```

We first create an instance of RootResourceBuilder, which we use to define resources at the root of our application, e.g. below the root URL "/".

The first line defines a new collection resource called "Albums" (the name is based on the controller name). This resource is configured within the lambda statement. We first tell RezRouting which controller(s) to use. Within the Albums configuration delegate, we define another collection resource (Photos), which will be nested within the Albums resource.

Finally, the MapRoutes method is called, passing the current application's RouteCollection.

The following routes are now available in the application:

TODO - screen grab from Glimpse or similar, once we have a demo app up and running.

## <a id="#common-resource-configuration"></a> Common Resource Configuration

### Controllers 

Use the `HandledBy` methods when configuring a resource to specify the controller(s) used to handle each action. 

You can specify single or multiple controllers, allowing you to partition a resource's actions into different controllers. It often makes sense to separate controller between read / write functionality as follows:

- AlbumsDisplayController - supports the Index and Show actions, e.g. read-only display of information
- AlbumsEditController - supports the New, Edit, Create, Update and Destroy actions. This makes sense as the view used for New and Edit is often very similar. Also, if an Update / Create fails due to invalid information, then the view is displayed again.

### Resource Naming

A resource's name is used in the name of the routes and in the path used in route URLs. RezRouting defaults to a built-in convention that bases each resource's name on the name(s) of the controller types registered. The name is then converted to a plural or singular, depending on whether routes are being configured for a collection or singular resource.

If a single controller has been specified, then the "Controller" is trimmed from the end of the type name to give us our resource name.

If multiple controllers are used, then the portion of the controller type name that is common to all controllers is used. For example, the value "Albums" would be extracted from the AlbumsDisplayController and AlbumsEditController type names. If a common name cannot be found, then a custom resource name should be supplied (see below) an exception is currently thrown when mapping routes if a common name cannot be found and no custom name is supplied. TODO - would it be better to fall back to the first controller?

You can override the resource name of an individual resource using the `CustomName` method as outlined below.

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsDisplayController, ProductsEditController>();
  products.CustomName("OurCoolProducts");
});
```

### Action Discovery

By default, RezRouting has 7 built in routes, each of which correspond to controller actions Index, Show, New, Create, Update, Edit and Destroy. You can change / add / remove these routes as we'll see below.

RezRouting maps all routes that are supported by the resource's controller types - there's no point in creating a route for the "Show" action if the action doesn't exist. Any public instance method that returns an ActionResult is treated as a candidate. The ASP.Net MVC ActionNameAttribute is also taken into account - routes will be matched based on the overridden ActionName if this is used.

If an identical action exists on more than one controllers, then the route will be mapped to the first controller specified in calls to the `HandledBy` method.

### Resource Paths

Each resource has a path segment that is used when building up the route URL. By default this is based on the resource name and is formatted using the default settings, which converts it to lower case.

### <a id="#home-route"></a> Mapping the "/" home route to a Controller

RootResourceBuilder can be configured in a similar way to child collections and singular resources. Using the existing conventions, a GET request for the path "/" would be mapped to a "Show" action on the controller. Simply specify the home controller using the `HandledBy` method above.

This still needs some refinement in the configuration API and may change.

Clearly, it only makes sense for you to set this up once.

### Common Resource Configuration Methods

|Method            |Description                              |Example|
|------------------|-----------------------------------------|-------|
|HandledBy         |Specifies one or more controller types   |       |
|CustomName        |Specifies a custom resource name         |       |
|CustomPath        |Sets a custom path to use in resource URLs|     |
|Include           |Specifies the actions that should be mapped|     |
|Exclude           |Specifies the actions that should not be mapped|  |
|IdName            |Specifies an alternative name for the id parameter in the route URL. By default {id} is used, but you might wish to rename it to match the property used to identify a resource, e.g. /users/{username}.| |
|IdNameAsAncestor|Supplies an alternative name for the id parameter of a resource in the route URL of a nested resource. By default the resource name is combined with "Id", e.g. albumId is used for Album resource.| |

 
## <a id="collection"></a>Collection Resource Routes
TODO

## <a id="singular"></a>Singular Resource Routes
TODO

##<a id="author">Author</a>
Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##<a id="contributors">Contributors</a>
Your name here!

##<a id="license">License</a>
RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
