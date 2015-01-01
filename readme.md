# <a id="top"></a>RezRouting.Net

RezRouting.Net configures routes for ASP.Net web applications using clean [resource-oriented](wiki/2.-Thinking-Resourcefully) URLs. 

ASP.Net web application frameworks like ASP.Net MVC and ASP.Net Web API have a very simple "flat" route structure by default, which doesn't suit complex applications or support a hierarchical route structure. Route setup can get clunky in larger applications and many applications would benefit from consistently structured routes with clean URLs.

Enter RezRouting.Net! 

- ASP.Net MVC support (ASP.Net Web API coming soon!)
- Nice URLs
- Suitable for both user-facing web applications and machine-to-machine APIs
- Create routes for singular resources (e.g. /session), collection and item resources (e.g. /users, /users/phoebe) and various nested combinations (e.g. /users/phoebe/reviews)
- Partition actions for a resource into separate controllers - no more bloated controllers!
- Routing convention for CRUD (create, read, update, delete) - similar to Ruby on Rails' [Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default)
- Another routing convention for task-centric URLs - a flexible scheme for more complex user-facing web applications
- An extensible route building mechanism that allows you to set up your own routing conventions
- Optimised helper methods for URL generation

## Example
The following route configuration (from the [ASP.Net MVC CRUD demo](https://github.com/danmalcolm/RezRouting.Net/tree/master/src/RezRouting.Demos.Crud)) sets up CRUD (create, read, update, delete)routes to handle singing in and managing a collection of products and reviews:

```C#
var builder = new ResourcesBuilder();
var conventions = new CrudRouteConventionBuilder();
builder.RouteConventions(conventions.Build());
builder.Singular("Session", session => session.HandledBy<SessionController>());
builder.Collection("Products", products =>
{
    products.HandledBy<ProductsController>();
    products.Items(product =>
    {
        product.HandledBy<ProductController>();
        product.Collection("Reviews", reviews =>
        {
            reviews.HandledBy<ReviewsController>();
            reviews.Items(review => review.HandledBy<ReviewController>());
        });
    });
});
builder.MapMvcRoutes(routes);
```

RezRouting configuration involves defining the structure of your application's resources, the routes that each resource supports and the controllers that handle the routes. Here, we're using a convention that looks for CRUD-focussed controller actions and creates routes for those that are supported by a resource's controllers. 

This creates the following routes in the application's RouteCollection:

|Route                           |HTTP Method & URL Template          | Action method / purpose  |
|--------------------------------|--------------------------------------|--------------------------|
|Session.Show                    |GET /session                          |SessionController.Index - Displays authentication status and sign in form if user not already logged in  |
|Session.Create                  |POST /session                         |SessionController.Create - Authenticates user (the sign in form posts to this action) |
|Session.Delete                  |DELETE /session                       |SessionController.Delete - Signs out the user |
|Products.Index                  |GET /products                         |ProductsController.Index - Display a list of products |
|Products.New                    |GET /products/new                     |ProductsController.New - Displays a form for new product details   |
|Products.Create                 |POST /products                        |ProductsController.Create - Saves a new product (new product form POSTs to this action |
|Products.Product.Show           |GET /products/{id}                    |ProductController.Show - Displays an individual product  |
|Products.Product.Edit           |GET /products/{id}/edit               |ProductController.Edit - Displays form to allow product to be edited   |
|Products.Product.Update         |PUT /products/{id}                    |ProductController.Update - Saves changes to product    |
|Products.Product.Delete         |DELETE /products/{id}            |ProductController.Delete - Deletes a product   |
|Products.Product.Reviews.Index  |GET /products/{productId}/reviews     |ReviewsController.Index - Displays all reviews for a given product |
|Products.Product.Reviews.New    |GET /products/{productId}/reviews/new |ReviewsController.New - |
|Products.Product.Reviews.Create |POST /products/{productId}/reviews    |ReviewsController.Create |
|Products.Product.Reviews.Show   |GET /products/{id}                    |ReviewController.Index  |
|Products.Product.Reviews.Edit   |GET /products/{id}/edit               |ReviewController.New    |
|Products.Product.Reviews.Update |PUT /products/{id}/edit               |ReviewController.New    |
|Products.Product.Reviews.Delete |DELETE /products/{id}/edit            |ReviewController.New    |


## Where Next?
The above example is just scratching the surface.

Download the source and try out the demos.

Read the WIKI for full background and documentation.

RezRouting was inspired by [Ruby on Rails Resource Routing](http://guides.rubyonrails.org/routing.html#resource-routing-the-rails-default) and its ASP.Net MVC port [Restful Routing](http://restfulrouting.com/). 

##<a id="author">Author</a>
Dan Malcolm [@lakescoder](http://twitter.com/lakescoder) - [blog](http://www.danmalcolm.com)

##<a id="contributors">Contributors</a>
Your name here! Add a feature, help with the documentation - all contributions welcome.

##<a id="license">License</a>
RezRouting.Net is released under the MIT License. See the [bundled LICENSE](https://github.com/MehdiK/RezRouting.Net/blob/master/LICENSE) file for details.
