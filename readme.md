# <a id="top"></a>RezRouting.Net

RezRouting sets up routes in ASP.Net web applications based on a flexible resource-oriented structure.

Features and benefits:

- ASP.Net MVC support (ASP.Net Web API coming soon!)
- Clean URLs, with a range of formatting options (lowercase, dashed, underscored etc.)
- Suitable for both user-facing web applications and APIs designed for machine-to-machine communication
- Support for singular resources, collections and various nested combinations, e.g. /products/27229/reviews
- Partition actions for a resource into separate controllers - no more bloated controllers and SRP violations
- Built-in CRUD routing scheme - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Built-in task-centric routing scheme - a flexible scheme for more complex applications
- An extensible route building mechanism that allows you to set up your own routing conventions



See [Thinking Resourcefully](#background) in the WIKI for further details on using a resource-centric URL structure.

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

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API offer a very simple route structure out-of-the-box. This doesn't scale well to more complex applications - it doesn't set a pattern for managing a hierarchy of resources. In addition, the controller-per-resource results in a large number of unrelated action methods on the same controller, resulting in bloated controllers.

RezRouting.Net provides a simple API for setting up resource-oriented routes in ASP.Net web applications. Here's some example code that you'd run at startup to map routes for an /albums collection resource, which, in turn contains a nested products collection:

```C#
var mapper = new RouteMapper();
mapper.Collection(products =>
{
  products.HandledBy<AlbumsController>();
  products.Collection(products => products.HandledBy<productsController>());
});
// Add to the application's RouteCollection
mapper.MapRoutes(RouteTable.Routes);
```

This would map web requests (based on HTTP method and URL path) to action methods on the specified controllers. Selected examples (not all) of the routes created are listed below:

- `GET /albums` displays a list of albums - AlbumsController.Index action
- `GET /albums/123` display an individual album - AlbumsController.Show action
- `POST /albums` creates a new album - AlbumsController.Create action
- `GET /albums/123/products` displays products in an individual album - productsController.Show action
- `DELETE /albums/123/products/456` removes product 456 from album 123 - productsController.Delete action

Setting up routes like this manually is tedious and error-prone. Let RezRouting do the work for you.

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). RezRouting's built-in CRUD routing scheme structures routes are structured in the same way as those created by Ruby on Rails out-of-the box. See [Background - Thinking Resourcefully](#background) below for further background on this type of URL structure or skip ahead to [getting started](#getting-started)

## <a id="background"></a>Background - Thinking Resourcefully

This section is intended for users new to the concept of REST, resources and routes - you can skim this section or skip ahead to [getting started](#getting-started) if you already _GET_ (cough) resourceful URLs and why you might want to use them.

[REST](http://en.wikipedia.org/wiki/Representational_state_transfer) is an architectural style that defines an approach to or machine-to-machine communication. From a purely practical point of view, structuring your URLs and controllers / actions in terms of your application's resources and the actions available on them fits a surprisingly wide range of scenarios. From an aesthetic point of view, the URLs look quite nice.

Put simply, a resource is a "thing" within your web application, a product, a product album, a product etc. It covers a range, from abstract concepts, a message /lakescoder/status/454604724755390464, a business entity /contracts. A "resourceful" approach to structuring your application involves using the web's document-oriented architecture to provide access to and perform actions on these things.

An application's resources are accessed using logically structured URL paths that represent the resources and the relationships between them. URLs like /users/phoebe, /users/phoebe/edit and /users/phoebe/albums are resourceful. URLs like "/scripts/albums.php?uid=2777&aid=36888" or "/index.cfm?fuseaction=albums..." are not... This replicates the web's document oriented structure our collection /users and resources /users/phoebe are like folders and HTML documents made available by a web server.

So, how are resourceful URLs structured? Let's look at some examples of resource-oriented URLs.

The following route URLs would be set up to display or provide access to a resource:

- GET /products - display the products resource collection
- GET /products?q=coffee - filter a collection of product resources
- GET /products/1234 - display an individual item within the products resource collection
- GET /products/1234/reviews - display the reviews belonging to a product resource (a collection of nested below an item within the products resource collection)
- GET /profile - display a singular profile resource, containing details of the currently signed-in user
- GET /profile/sessions - display a history of the currently signed-in user's recent sessions (a resource collection nested within a singular resource)

In the context of a user-facing web application, these URLs would provide access to web pages containing HTML content representing the resource. In an API designed for machine-to-machine communication, the resource data would be made available in a data format like JSON or XML.

RezRouting is structured around 3 levels or types of resources:

- A _singular_ resource refers to a single "thing" or entity in your application. Singular resources are often implicitly identified by the user's authentication status, e.g. a URL like /profile identifies the profile associated with the currently authenticated user. /shopping-cart might identify the list of items added to a basket tied to a user's session on an e-commerce site.
- A _collection_ resource contains multiple "things" or entities. The collection itself might have child resources. Named in the plural, collections look like a directory in the URL, e.g. /products. 
- A _collection item_ - Both the collection itself and the items within it are resources in their own right. Collection items are nested within their parent collection and identified by an identifier within the URL, /products.

NOTE: RezRouting explicitly separates collection from collection items - each have their own set of controllers and routes. A number of design

Often, resources are structured within others, using a range of combinations of singular / collection item resources. If our simple product application was structured around product albums, we might make an albums resource available at _/albums_, with products nested beneath.

GET /products/123 - show details of album with ID 123
GET /products/123/reviews - show list of products in album 123
GET /albums/123/products/new - add details of a new product to add to album 123

The same structure works well in other scenarios. A sequence of screens used to input details of an order into a CRM might be structured as follows:

GET  /orders/new - screen to start entering details of a new order
POST /orders - save new order
GET  /orders/123/customer/edit - edit the customer details for order (which has an id of 123)
GET  /orders/123/shipping/edit - edit the shipping address
PUT  /orders/123/shipping/edit - save the shipping address
GET  /orders/123/payments/new - enter details of a new payment to record against the order

As well as providing access to resource representations, resource-centric routes can also be used to perform actions on resources.

Here are a few examples of route URLs that might be set up for a simple CRUD-based web application:

- GET /products/new - display a form used to create a new resource in the products collection
- POST /products - add an item to the products collection (the /products/new form is set to POST to this URL)
- GET /products/1234/edit - display a form used to edit an individual item within the collection
- PUT /products/1234 - update an item in the products resource collection (based on data in the /products/1234/edit form)
- GET /sessions/new - display a form used to sign in to a web site - at the time of writing Twitter uses this URL for it's sign-in form
- POST /sessions - authenticate and create a new session - as above, Twitter's sign-in form POSTs to /sessions

For a more complex user-facing web application, a task-based URL structure offers more flexibility when the operations available don't match CRUD semantics:

- GET /products/1234/publish - task screen, used to initiate the publish task of a product resource
- POST /products/1234/publish - perform the publish task (using the details specified in the  /products/1234/publish form)
- GET /products/1234/archive - task screen, used to initiate archiving of a product resource
- POST /products/1234/archive - performs the archive task (using the details specified in the  /products/1234/publish form)
- GET /products/bulk-create - task screen used to add multiple items to the products resource collection
- POST /products/bulk-create - performs the bulk-create task
- GET /products/reviews/approve - task screen used for bulk management of reviews added to all products
- POST /products/reviews/approve - performs the approve task based on form data submitted

Although the use of verbs in URLs isn't recommended for machine-to-machine REST services (Google it!), they are highly appropriate to user-facing web applications. Generally, you should forget about REST in the context of user-facing web apps - well, apart from stealing the idea about how you should structure your resources.




## <a id="getting-started"></a>Getting Started

RezRouting is available on [nuget](https://nuget.org/packages/RezRouting):

`Install-Package RezRouting`

Once you have RezRouting referenced, you need to configure your routes at application start-up. You might include your configuration code in one of the following places:

- A static RouteConfig.Init method in App_Start\RouteConfig.cs file that you call from Global.as
- Directly within the Application_Start method in Global.asax (might be OK for a small app)
- In a larger application, you might have separate parts of your application set up their own routes separately. You can incorporate RezRouting easily into any existing start-up mechanism, each part of the application can create it's own RouteMapper instance and set up its routes independently.

Routes are initialised using an instance of the RouteMapper class as follows:

```C#

```
We first create an instance of RouteMapper, RezRouting's entry point for defining resource routes within our application.

Finally, the MapRoutes method is called, passing the current application's RouteCollection.

The following routes are now available in the application:

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
|Index  |GET      |/albums            |AlbumsController.Index   |Display a list of all products |
|New    |GET      |/albums/new        |AlbumsController.New     |Display form for creating a new item |
|Create |POST     |/albums	          |AlbumsController.Create  |Create a new product |
|Show   |GET      |/albums/{id}	      |AlbumsController.Show	|Display a specific product |
|Edit   |GET      |/albums/{id}/edit  |AlbumsController.Edit	|Display for for editing specific item |
|Update |PUT      |/albums/{id}	      |AlbumsController.Update	|update a specific product |
|Delete |DELETE   |/albums/{id}	      |AlbumsController.Destroy	|delete a specific product|

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
  albums.Collection(products => products.HandledBy<productsController>());
});
```

|Route  |Route Name           |HTTP Verb|URL Path                           |Controller Action        |Purpose|
|-------|---------------------|---------|-----------------------------------|-------------------------|-------------|
|Index  |Albums.products.Index  |GET      |/albums/{albumId}/products           |AlbumsController.Index   |Display a list of all products |
|New    |Albums.products.New    |GET      |/albums/{albumId}/products/new       |AlbumsController.New     |Display form for creating a new item |
|Create |Albums.products.Create |POST     |/albums/{albumId}/products	        |AlbumsController.Create  |Create a new product |
|Show   |Albums.products.Show   |GET      |/albums/{albumId}/products/{id}	    |AlbumsController.Show	  |Display a specific product |
|Edit   |Albums.products.Edit   |GET      |/albums/{albumId}/products/{id}/edit |AlbumsController.Edit	  |Display for for editing specific item |
|Update |Albums.products.Update |PUT      |/albums/{albumId}/products/{id}	    |AlbumsController.Update  |update a specific product |
|Delete |Albums.products.Delete |DELETE   |/albums/{albumId}/products/{id}	    |AlbumsController.Destroy |delete a specific product|

The routes names and URLs paths are based on the nested resource hierarchy. Note that the id of the parent resource within the route URL uses a name that relates child to parent.

Here are examples of the parent resource and child resource controllers

```C#
public class AlbumsController
{
	public ActionResult Index() { // ... }

    public ActionResult Show(int id) { // ... }
}

public class productsController
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
