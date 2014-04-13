# <a id="top"></a>RezRouting.Net

RezRouting.Net creates ASP.Net MVC routes that map resource-oriented URLs to controller actions. It features:

- Simple configuration API and convention-based naming - it's usually a one-liner to map routes for each type of resource
- Clean URLs, with lots of formatting options (lowercase, uppercase (surely not!), dashed, underscored)
- Support for nested routes, e.g. /photos/27229/comments
- Use separate controllers for different resource actions
- Smart action discovery - only maps the routes supported by actions available on each controller

## Contents
 - [Introduction](#intro)
 - [Background - Thinking Resourcefully](#background)
 - [Getting Started](#getting-started)
 - [Overview](#overview)
 - [Mapping the "/" home route](#home-route)
 - [Collection Resource Routes](#collection)
 - [Singular Resource Routes](#singular)
 - [Adding Custom Routes](#singular)
 - [General Configuration](#configuration)
 - [Author](#author)
 - [License](#license)

## <a id="intro"></a>Introduction

RezRouting.Net provides a simple API for setting up resource-oriented routes in an ASP.Net MVC web application. Here's some example route configuration code that maps routes for an /albums collection resource, which, in turn contains a nested photos collection:

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

`GET /albums` displays a list of albums - AlbumsController.Index action
`GET /albums/123` display an individual album - AlbumsController.Show action
`POST /albums` creates a new album - AlbumsController.Create action
`GET /albums/123/photos` displays photos in an individual album - PhotosController.Show action
`DELETE /albums/123/photos/456` removes photo 456 from album 123 - PhotosController.Delete action

Setting up routes like this manually is tedious and error-prone. Let RezRouting do the work for you.

RezRouting was inspired by [Ruby on Rails RESTful Routing](http://guides.rubyonrails.org/routing.htmlrouting) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). RezRouting's default routes are structured in the same way as those created by Ruby on Rails out-of-the box. See [Background - Thinking Resourcefully](#background) below for further background on this type of URL structure or skip ahead to 

## <a id="background"></a>Background- Thinking Resourcefully

Forget the theoretical discussions about [REST](http://en.wikipedia.org/wiki/Representational_state_transfer) for now. From a purely practical point of view, structuring your web application's URLs and controllers / actions in terms of resources and collections of resources works well in a surprisingly wide range of scenarios. 

The following is intended for users new to the concept of REST, resources and routes - you can skim this section or skip ahead to the <a href="#getting-started">next section</a> if you already "get" resources (ho-ho - PRs to remove dubious humour always accepted).

So, how are resourceful URLs structured?

Imagine a simple web application used to maintain a list of photos. A resource-oriented URL / controller structure would display photo information using the URL _/photos_ and use a _PhotosController_ controller to provide the functionality. Different URLs and HTTP methods would be used to access different controller actions. The URLs for the common CRUD (create, read, update and delete) actions and the way in which they relate to controller actions are displayed below.

|HTTP Verb|URL Path           |Controller#Action|Purpose|
|---------|------------------ |-----------------|-------------|
|GET      |/photos            |photos#index     |Display a list of all photos |
|GET	  |/photos/new        |photos#new       |Display form for creating a new item |
|POST	  |/photos	      |photos#create    |Create a new photo |
|GET	  |/photos/{id}	      |photos#show	|Display a specific photo |
|GET	  |/photos/{id}/edit  |photos#edit	|Display for for editing specific item |
|PUT	  |/photos/{id}	      |photos#update	|update a specific photo |
|DELETE	  |/photos/{id}	      |photos#destroy	|delete a specific photo|

Don't be put off too much by the apparent restrictions that this CRUD structure might enforce. A complex system can still be modelled around resourceful URLs.

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

###<a id="configuration-routenames">Route Name Discovery and Formatting</a>

###<a id="configuration-routepath">Route Path Format</a>

##<a id="home-route">Home Page Route</a>

##<a id="collection">Collection Resource Routes</a>

For example, the following routes would be created for our "photos" resource:

##<a id="singular">Singular Resource Routes</a>


##<a id="author">Author</a>
Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##<a id="contributors">Contributors</a>
Your name here!

##<a id="license">License</a>
RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
