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
 - [Mapping Collection Resources](#collections)
 - [Mapping Singular Resources](#singular)
 - [Resource Configuration Options](#common-resource-configuration)
  - [Resource Naming](#resource-naming)

 - [Adding Custom Routes](#singular)
 - [General Configuration](#configuration)
 - [Author](#author)
 - [License](#license)

## <a id="intro"></a>Introduction

RezRouting.Net provides a simple API for setting up resource-oriented routes in an ASP.Net MVC web application. Here's some example code that you'd run at startup to map routes for an /albums collection resource, which, in turn contains a nested photos collection:

```C#
var mapper = new RouteMapper();
mapper.Collection(albums =>
{
  albums.HandledBy<AlbumsController>();
  albums.Collection(photos => photos.HandledBy<PhotosController>());
});
// Add to the application's RouteCollection
mapper.MapRoutes(RouteTable.Routes);
```

This would map web requests (based on HTTP method and URL path) to action methods on the specified controllers. Selected examples (not all) of the routes created are listed below:

- `GET /albums` displays a list of albums - AlbumsController.Index action
- `GET /albums/123` display an individual album - AlbumsController.Show action
- `POST /albums` creates a new album - AlbumsController.Create action
- `GET /albums/123/photos` displays photos in an individual album - PhotosController.Show action
- `DELETE /albums/123/photos/456` removes photo 456 from album 123 - PhotosController.Delete action

Setting up routes like this manually is tedious and error-prone. Let RezRouting do the work for you.

RezRouting was inspired by [Ruby on Rails RESTful Routing](http://guides.rubyonrails.org/routing.htmlrouting) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). RezRouting's default routes are structured in the same way as those created by Ruby on Rails out-of-the box. See [Background - Thinking Resourcefully](#background) below for further background on this type of URL structure or skip ahead to [getting started](#getting-started)

## <a id="background"></a>Background - Thinking Resourcefully

This section is intended for users new to the concept of REST, resources and routes - you can skim this section or skip ahead to [getting started](#getting-started) if you already _get_ resourceful URLs and why you might want to use them (ha ha - PRs to remove dubious humour always accepted).

Forget the theoretical discussions about [REST](http://en.wikipedia.org/wiki/Representational_state_transfer) for now. From a purely practical point of view, structuring your URLs and controllers / actions in terms of your application's resources and the actions available on them fits a surprisingly wide range of scenarios. From an aesthetic point of view, the URLs look quite nice.

Put simply, a resource is a "thing" within your web application, a product, a photo album, a photo etc. A "resourceful" approach to structuring your application involves uses existing web architecture and conventions to allow users to access and perform actions on these things.

- Firstly, an application's resources are accessed using logically structured URL paths that represent the resources and the relationships between them. URLs like /users/phoebe, /users/phoebe/edit and /users/phoebe/albums are resourceful. URLs like "/scripts/albums.php?uid=2777&aid=36888" or "/index.cfm?fuseaction=albums..." are not... This replicates the web's document oriented structure our collection /users and resources /users/phoebe are like folders and HTML documents made available by a web server.

- Secondly, the actions that a user can perform upon these resources are based around HTTP methods (again we're using an existing web mechanisms). HTTP methods are based on the web's document oriented roots, but they work quite well for resources in your application too. `GET /users/phoebe/albumns` displays an HTML page containing a list of a user's photos, `POST /users/phoebe/photos` adds a new photo, `PUT /users/phoebe/photos/123` updates a specific photo.

So, how are resourceful URLs structured?

Imagine a simple web application used to maintain a list of photos. A resource-oriented URL / controller structure would display photo information using the URL _/photos_, routing requests to actions on a _PhotosController_ controller. Different URLs and HTTP methods would be used to access different controller actions. The URLs for the common CRUD (create, read, update and delete) actions and the way in which they relate to controller actions are displayed below.

|HTTP Verb|URL Path           |Controller#Action|Purpose|
|---------|------------------ |-----------------|-------------|
|GET      |/photos            |photos#index     |Display a list of all photos |
|GET	  |/photos/new        |photos#new       |Display form for creating a new item |
|POST	  |/photos	          |photos#create    |Create a new photo |
|GET	  |/photos/{id}	      |photos#show	    |Display a specific photo |
|GET	  |/photos/{id}/edit  |photos#edit	    |Display for for editing specific item |
|PUT	  |/photos/{id}	      |photos#update	|update a specific photo |
|DELETE	  |/photos/{id}	      |photos#destroy	|delete a specific photo|

Don't be put off too much by the restrictions that this CRUD structure might enforce. A complex system can still be modelled around resourceful URLs.

Resources come in 2 types, based on plurality:

- A _singular_ resource refers to a single "thing" or entity in your application. Singular resources have a URL like /session
- A _collection_ resource contains multiple "things" or entities. Both the collection itself and the items within it are resources in their own right. The collection itself has a URL like /users. Individual resources within a collection have an identifier within the URL, e.g. /users/2920 or /users/phoebe.

Often, resources are structured within others. If our simple photo application was structured around photo albums, we might make an albums resource available at _/albums_, with photos nested beneath.

GET /albums/123 - show details of album with ID 123
GET /albums/123/photos - show list of photos in album 123
GET /albums/123/photos/new - add details of a new photo to add to album 123

The same structure works well in other scenarios. A sequence of screens used to input details of an order into a CRM might be structured as follows:

GET  /orders/new - screen to start entering details of a new order
POST /orders - save new order
GET  /orders/123/customer/edit - edit the customer details for order (which has an id of 123)
GET  /orders/123/shipping/edit - edit the shipping address
PUT  /orders/123/shipping/edit - save the shipping address
GET  /orders/123/payments/new - enter details of a new payment to record against the order

## <a id="getting-started"></a>Getting Started

RezRouting is available on [nuget](https://nuget.org/packages/RezRouting):

`Install-Package RezRouting`

Once you have RezRouting referenced, you need to configure your routes at application start-up. You might include your configuration code in one of the following places:

- A static RouteConfig.Init method in App_Start\RouteConfig.cs file that you call from Global.as
- Directly within the Application_Start method in Global.asax (might be OK for a small app)
- In a larger application, you might have separate parts of your application set up their own routes separately. You can incorporate RezRouting easily into any existing start-up mechanism, each part of the application can create it's own RouteMapper instance and set up its routes independently.

Routes are initialised using an instance of the RouteMapper class as follows:

```C#
var mapper = new RouteMapper();
mapper.Collection(albums =>
{
  mapper.HandledBy<AlbumsController>();
  mapper.Collection(photos => photos.HandledBy<PhotosController>());
});
// Add to the application's RouteCollection
mapper.MapRoutes(RouteTable.Routes);
```
We first create an instance of RouteMapper, RezRouting's entry point for defining resource routes within our application.

The first line defines a new collection resource called "Albums" (the name is based on the controller name). This resource is configured within the lambda statement. We first tell RezRouting which controller(s) to use. Within the Albums configuration delegate, we define another collection resource (Photos), which will be nested within the Albums resource.

Finally, the MapRoutes method is called, passing the current application's RouteCollection.

The following routes are now available in the application:

TODO - screen grab from Glimpse or similar, once we have a demo app up and running.

## <a id="#common-resource-configuration"></a> Common Resource Configuration

The following mechanisms are available when defining a collection or singular resource

### <a id="collection-resources"></a>Mapping Collection Resources

Collection resource routes are mapped using the `Collection` method. You would map routes for a collection of album resources as follows:

```C#
mapper.Collection(x => x.HandledBy<AlbumsController>());
```

The standard collection routes are as follows:

|Route  |HTTP Verb|URL Path           |Controller Action        |Purpose|
|-------|---------|-------------------|-------------------------|-------------|
|Index  |GET      |/albums            |AlbumsController.Index   |Display a list of all photos |
|New    |GET      |/albums/new        |AlbumsController.New     |Display form for creating a new item |
|Create |POST     |/albums	          |AlbumsController.Create  |Create a new photo |
|Show   |GET      |/albums/{id}	      |AlbumsController.Show	|Display a specific photo |
|Edit   |GET      |/albums/{id}/edit  |AlbumsController.Edit	|Display for for editing specific item |
|Update |PUT      |/albums/{id}	      |AlbumsController.Update	|update a specific photo |
|Delete |DELETE   |/albums/{id}	      |AlbumsController.Destroy	|delete a specific photo|

Note that the collection is a resource in itself, as well as the resources within it. Index, New and Create apply to the collection itself. Routes that have an id in the URL reference individual items within the collection (Show, Edit, Update and Delete)

### <a id="singular-resources"></a>Mapping Singular Resources

Routes for a singular resource are mapped using the `Singular` method. The routes are similar to collection routes, but:

- They don't have an _Index_ route. You can't view a list of a singular resource.
- An identifier is not included in the URL. 

Singular resources are used for a resource that only has one logical instance, e.g. /settings. They are also used for a single resource is identified by the context of the web request, e.g. /profile might display details of the logged in user based on the current user's authentication cookie.

The standard singular routes are as follows:

|Route  |HTTP Verb|URL Path           |Controller Action        |Purpose|
|-------|---------|------------------ |-------------------------|-------------|
|New    |GET	  |/profile/new       |ProfileController.New    |Display form for creating a new profile |
|Create |POST	  |/profile	          |ProfileController.Ceate  |Create a new profile |
|Show   |GET	  |/profile	          |ProfileController.Show	|Display the current profile |
|Edit   |GET	  |/profile/edit      |ProfileController.Edit	|Display form for editing the current profile |
|Update |PUT	  |/profile	          |ProfileController.Update	|Update the current profile |
|Delete |DELETE	  |/profile	          |ProfileController.Destroy|Delete the current profile |

### Choosing the Controllers that Handle a Resource's Actions

Use the `HandledBy` methods when configuring a resource to specify the controller(s) used to handle each action. 

```C#
mapping.Collection(x => x.HandledBy<AlbumsController>());
```

You can specify single or multiple controllers, allowing you to partition a resource's actions into different controllers. It often makes sense to separate controller between read / write functionality as follows:

```C#
mapping.Collection(x => x.HandledBy<AlbumsDisplayController, AlbumsEditController>());
```

- AlbumsDisplayController - supports the Index and Show actions, e.g. read-only display of information
- AlbumsEditController - supports the New, Edit, Create, Update and Destroy actions. This makes sense as the view used for New and Edit is often very similar. Also, if an Update / Create fails due to invalid information, then the view is displayed again.

### Action Discovery - Selecting the Routes Mapped by the Controllers

When RezRouting comes to set up the routes, it maps only the routes that have corresponding actions on the controller types - there's no point in creating a route for the "Show" action if the Show action doesn't exist. The ASP.Net MVC ActionNameAttribute is also taken into account - routes will be matched based on the overridden ActionName if this is used. If an identical action exists on more than one controller, then the route will be mapped to the first controller specified in calls to the `HandledBy` method.

### Resource Naming

A resource's name is used in the name of the routes and in the path used in route URLs. For example, the routes for an albums collection resource will be called "Albums.Index", "Albumns.Show" etc and the route URLs will include the path segment "/albums".

RezRouting defaults to a built-in convention that bases each resource's name on the name(s) of the controller types registered. 

If a single controller has been specified, then the "Controller" part is trimmed from the end of the type name to give us our resource name.

If multiple controllers are used, then the portion of the controller type names that is common to all controllers is used, e.g. "Albums" would be extracted from the "AlbumsDisplayController" and "AlbumsEditController". If a common name cannot be found, then the name is based upon the first controller type.

The name is then converted to a plural or singular, depending on whether routes are being configured for a collection or singular resource. Note that this uses the clever [Inflector](#) library which does a pretty good job and gets the plurals / singular right most of the time for normal words. If you're using terms that don't fit, consider using a [custom resource name].

You can override the resource name of an individual resource using the `CustomName` method as outlined below.

### Resource URLs 

Each resource has a path segment that is used when building up the route URL. By default this is based on the resource name and is formatted using the default settings, which converts it to lower case.

### Home Page Controller - Mapping Routes for the / request


### Nested Resources

Many systems will have child resources (singular or collection) that belong to a parent resource.

Routes for nested resources are configured in the same way as top-level resources, but within the configuration delegate of the parent:

```C#
mapper.Collection(albums =>
{
  albums.HandledBy<AlbumsController>();
  albums.Collection(photos => photos.HandledBy<PhotosController>());
});
```

|Route  |Route Name           |HTTP Verb|URL Path                           |Controller Action        |Purpose|
|-------|---------------------|---------|-----------------------------------|-------------------------|-------------|
|Index  |Albums.Photos.Index  |GET      |/albums/{albumId}/photos           |AlbumsController.Index   |Display a list of all photos |
|New    |Albums.Photos.New    |GET      |/albums/{albumId}/photos/new       |AlbumsController.New     |Display form for creating a new item |
|Create |Albums.Photos.Create |POST     |/albums/{albumId}/photos	        |AlbumsController.Create  |Create a new photo |
|Show   |Albums.Photos.Show   |GET      |/albums/{albumId}/photos/{id}	    |AlbumsController.Show	  |Display a specific photo |
|Edit   |Albums.Photos.Edit   |GET      |/albums/{albumId}/photos/{id}/edit |AlbumsController.Edit	  |Display for for editing specific item |
|Update |Albums.Photos.Update |PUT      |/albums/{albumId}/photos/{id}	    |AlbumsController.Update  |update a specific photo |
|Delete |Albums.Photos.Delete |DELETE   |/albums/{albumId}/photos/{id}	    |AlbumsController.Destroy |delete a specific photo|

The routes names and URLs paths are based on the nested resource hierarchy. Note that the id of the parent resource within the route URL uses a name that relates child to parent.

Here are examples of the parent resource and child resource controllers

```C#
public class AlbumsController
{
	public ActionResult Index() { // ... }

    public ActionResult Show(int id) { // ... }
}

public class PhotosController
{
    public ActionResult Index(int albumId) { // ... }

    public ActionResult Show(int albumId, int id) { // ... }
}
```




## Resource Configuration Options

A number of options can be used to customise the way that the routes for a specific resource are mapped. These are one-off settings that apply to a specific resource.

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsController>(); // Set the controllers used
  products.CustomName("OurCoolProducts"); // Use a different name in the routes (and the URL is)
  products.CustomPath("our-super-cool-products"); // use a different path in the URL
  products.Include("Index", "Show"); // Only map the Index and Show routes - or products.Exclude("Destroy") - map all routes except destroy
  products.IdName("code"); // Use an alternative to "id" in the route URL template, e.g. /products/{code} instead of the default /products/{id}
  products.IdNameAsAncestor("coolProductCode"); // Use a different name for the id parameter in nested route URLs
});

```

### HandledBy - Specify the Controller Types used for a Resource

Variations exist to make this concise. You can use a generic type parameter or a Type parameter. Described in detail in the [Choosing the Controllers that Handle a Resource's Actions](#) above.

### CustomName - Set a Custom Resource Name

The default resource name is based on the names of the controller types used - see [Resource Naming](#resource-naming)above. Use the `CustomName` method to use a different name instead. 

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsController>(); 
  products.CustomName("OurCoolProducts"); 
});

```

The path segment in the resource's route URLs will also be based on this custom name, formatted using the default formatting mechanism.

Note also that custom resource names are used as-is and are not converted to singular / plural.

### CustomPath - Use a Custom Path in Resource URLs

The default path segment used in the URL is based on the name of the resource. It can be overriden if you want more control over the URL:

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsController>(); 
  products.CustomPath("our_cool_products_its_nice"); 
});

```

### Include / Exclude - Limit the Routes Mapped

You might not want to map all of RezRouting's standard routes on certain resources, for example you may wish to omit the Delete route on certain types of resource. 

You can limit the routes mapped using include, e.g. the following would map only the Index and Show routes:

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsController>();
  products.Include("Index", "Show");
});

```

Use Exclude to map all routes except those specified.

```C#
root.Collection(products =>
{
  products.HandledBy<ProductsController>();
  products.Exclude("Delete");
});

```

### IdName - Set the Id Parameter in Route URLs

Route URLs for collection resources include a placeholder for the resource identifier in the Show, Edit, Update and Delete routes. RezRouting routes uses the name "id" for the identifier by default, e.g. the URL for the Show action is /products/{id}. This controls the name of the route value used by ASP.Net MVC ModelBinding and the names of parameters in your controller actions.

```C#
    ActionResult Show(string id)
    {
       // ... get data and render view
    }

```

You can override this using the IdName method, for example:

```C#
root.Collection(users =>
{
  users.HandledBy<UsersController>();
  users.IdName("username");
});

```

This would result in routes URLs like /users/{username}.

```C#
ActionResult Show(string username)
{
    // ... get data and render view
}

```

### IdNameAsAncestor - Set the Id Parameter in Nested Resource Route URLs

This works in a similar way to above, but 


 

##<a id="author">Author</a>
Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##<a id="contributors">Contributors</a>
Your name here! Add a feature, help with the documentation - all contributions welcome.

##<a id="license">License</a>
RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
