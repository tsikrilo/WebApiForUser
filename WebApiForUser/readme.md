## Back-end development

https://localhost:44384/api/Users/ is the url about json requests to web api application named WebApiForUser.

Included:
	- A repository wrapping UserController's methods.
	- Controller about UserTitle model in order to fetch table data from db
		(I couldn't make it work using Include at context)
	- Controller about UserTypes model in order to fetch table data from db
	- Logging mechanism
	- Unit tests

## Front-end Development

Application named UserApp running at http://localhost:4200/.

Included:
	- Components:
		- HomeComponent : Implements home page
		- UsersComponent : Implements users page (listing and user details)
		- MapViewComponent : Implements map view page
	- Services:
		- user-detail : includes methods which consume web api functionality
		- notification : used to show notifications when an action performed
	- Pipes:
		- filter.pipe : used to implement search at user list

## Tools used

Visual Studio 2019, Visual Studio Code, Microsoft SQL 2019.