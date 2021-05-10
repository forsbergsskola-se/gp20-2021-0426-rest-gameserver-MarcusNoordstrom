# GP20-2021-0426-Rest-Gameserver

## Goal of this Assignment
The goal of this assignment is to introduce you to developing REST-APIs using `C#`, `HTTP`, `JSON` and `ASP .NET Core`\
In total, our steps will include:
- An Acme.com Weblink Browser
- Exploring an existing REST-API through HTTP
- Building a Bikesharing Console Application
- Building a REST Game App

## Grading
|Grade  |  Requirement |
|-------|:-------------|
|Failed (F)| Everyone\* |
-------------------------------
\*Just kidding, of course. I'm still working on this :P


## Prerequisites / Requirements
- Make sure, that .NET Core 5 SDK is installed from https://www.microsoft.com/net/download
- I recommend to use Jetbrains Rider as an IDE.\
- Install Unity Hub & Unity.

## Remarks
- In the first Exercise, we are not using any HTTP-Classes, but manually using the HTTP-Protocol with a TCP-Client for educational purposes.


## Part 1 - Tiny Browser:

<img width="579" alt="image" src="https://user-images.githubusercontent.com/7360266/116148852-bcc7df80-a6e1-11eb-9282-370e37c97fc6.png">



### Goal
To have a Acme.com Weblink Browser that prints the current page title as well as a navigatable list of all Links that can be found on the page.

### Preparing a Project
Create a folder named `TinyBrowser`\
Open the Terminal in that Folder
Now, use the command `dotnet new console`\
If it says `dotnet not found`, you have probably not installed .NET Core 5 SDK, yet.\
Else, this command should have created a new C# Project for you. You can go ahead and open the `.csproj`-File in your IDE.\

Add a `.gitignore` in your `TinyBrowser`-Folder that ignores anything you don't want to commit.\
For C# Console Projects, that's at least the `/bin/` and `/obj/`-Folders.\
Afterwards, you may safely go ahead and create a new commit `adds tiny browser project`

### Implementation
- You will need: 
- The `TcpClient`-class which can be created by using its constructor together with arguments for the host name as well as the port number.
  - `GetStream` again gets you the current stream used for the client. It returns a `Stream`.
  - `Close` needs to be called when you are done using the `TcpClient`.
- The `Stream`-class is returned by `GetStream`
  - `Write` allows you to send Bytes over the socket. (Consider using `StreamWriter` though)
  - `Read` allows you to read Bytes over the socket. (Consider using `StreamReader` though)
  - `Close` needs to be called when you are done sending bytes over the stream.
- The `StreamWriter`-class has a constructor that you need to pass a `Stream` into.
  - `Write` allows you to send a string or any other data over the socket.
- The `StreamReader`-class has a constructor that you need to pass a `Stream` into.
  - `ReadToEnd` allows you to read a full string from the socket.
- `Encoding.ASCII.GetBytes` Can convert a `string` to ASCII-`byte[]` for you.
- `Encoding.ASCII.GetString` Can convert a `byte[]` to a `string`.
- `string`
  - `IndexOf(string value, int startIndex)` 
    - Can Find the Index at which a `string` can be found within another. 
    - Returns `-1` if no results were found.
    - e.g.: "Hello World".IndexOf("ll") will return 2.
    - e.g.: "Hello World".IndexOf("Planet") will return -1.
  - `Substring(int startIndex, int length)`
    - Returns the substring of a string between character number `startIndex` and `statIndex + length`.
    - e.g.: "Hello World".Substring(3, 4) will return "lo W";
  - `Path.Combine(string a, string b)`
    - Returns a combined path of a and b. handles dot-notiation `.` and `..` correctly automatically.

So, what is our server supposed to do?
- Send a TCP Request to acme.com using Port 80
- Using the HTTP Protocol
  - I recommend using HTTP 1.1.
  - Make sure to follow the Exact guidelines.
  - Every line is supposed to end with `CRLF` (carriage return). In C# that's `"\r\n"`
  - This is, what a HTTP/1.1-Request might look like:
```
GET / HTTP/1.1
Host: google.com

```
  - Especially don't forget the empty line at the end of your request and the Host-Header :)
- Use a TCP Client.
- Get the Stream.
- Write a valid HTTP-Request to the Stream.
- Fetch the response from the Website
- Search the respone for an occurence of `<title>
    - `<title>` is the start tag of an HTML `title`-Element used for page titles (visible on tabs) in browsers
    - `</title>` is the end tag of an HTML `title`-Element
    - Everything inbetween is the HTML-Content of the Element
    - And in this case, the title of the website
    - Print that string (between `<title>` and `</title>`) to the console.
- Search the response for all occurences of `<a href ="`
  - One sample: `<a href="auxprogs.html">auxiliary programs</a>`
  - Without going into too much detail:
    - `<a>` is the start tag of an HTML `hyperlink`-Element used for clickable links in browsers
    - `href="..."` is an HTML url-Attribute used to give the URL to the Hyperlink
    - `</a>` is the end tag of an HTML `hyperlink`-Element
    - Everything inbetween is the HTML-Content of the Element
    - And in this case, describes the Display Text of the Hyperlink
- For each occurence:
  - Find all letters until the next `"`-symbol.
  - These letters define the local URL to the destination
  - Remember this, so you can navigate to that URL, if the User decides to follow this link
  - Navigate to the next `>`-symbol, so you find the end of the start tag.
  - Every letter until the next occurence of `</a>` are part of the display text.
- Now, when you have all the information (display text & url for each link)
- Print them all to the console
  - Recommendation: Use an iterator i, starting at 0.
  - Iterate over a list of all information that you have stored before.
  - Print: `%INDEX%: %DISPLAYNAME% (%URL%)`, e.g.: `3: auxiliary programs (auxprogs.html)`
- Ask the user for Input
  - it should be a Number between 0 and the number of options
  - Follow the link that the user wants to follow and start at the beginning of the application again
  - (Send a TCP Request to acme.com...)
  - There is a few cases of URLs to consider. Some of them might be links, but...:
    - not to another web page, e.g. `<a href="image.png">` might be a link to an image.
      - i suggest skipping these links
    - to another host, e.g. `<a href="http://google.com/search/settings">`
      - replace the host with `google.com` and the path with `/search/settings/`
    - to a local url, e.g. `<a href="search"> when currently being at host `acme.com` and the path `/hello/world/`
      - keep the host and replace the path with `/hello/world/search/`
    - to a parent url, e.g. `<a href="../another"> when currently being at host `acme.com` and the path `/hello/world/`
      - keep the host and simply replace the path with `/hello/world/../another/` or `/hello/another/`




### Bonus:
- Prettify the Output: Replace any link description that's longer than 15 chars with a shorter version of the first and last 6 chars and ... in the middle.
  - e.g.: `"HelloMyPrettyWorld"` becomes `"HelloM..yWorld"`
- Implement a Back-Button: If the User inputs 'b' for Back, go back (to the previously visited Website.
  - Make sure, to not go forward, when going back twice :)
- Implement a Forward-Button: If the User inputs 'f' for Forward, go forward.
  - Make sure, that there is a website to go forward to :)
- Implement a Refresh-Button: If the User inputs 'r' for Refresh, refresh the page.
  - Make sure, that this won't spam the 'go back' history.
- Implement a History-Button: If the User inputs 'h' for History, he can see websites that he has visited.
  - As well as the date, when the page was opened.
  - If the User visits Website A, then B, then goes back to A, the History should show A, B, A. Not only A.
  - In other words, this history has to be separate from the Back-History.
- Implement a Goto-Button: If the User inputs 'g' for Goto, he can afterwards enter a URL of his own.
- Investigate options of using `XMLReader` instead of searching the `HTML`-Response manually.
  - Do this optional (as in replacable with interfaces)
  - So that I can see, that you also got a solution working
  - Where you manually search the string


## Part 2: GitHub Explorer

<img width="703" alt="image" src="https://user-images.githubusercontent.com/7360266/116456208-46062000-a862-11eb-8bd0-566e7939c265.png">


### Goal
To have a small GitHub Repository Browser by accessing GitHub's public REST API to receive information.

### Preparing a Project
Create a folder named `GitHubExplorer`\
Open the Terminal in that Folder
Now, use the command `dotnet new console`\
Add a `.gitignore` in your `GitHubExplorer`-Folder that ignores anything you don't want to commit.\
For C# Console Projects, that's at least the `/bin/` and `/obj/`-Folders.\
Afterwards, you may safely go ahead and create a new commit `adds github explorer project`

### Preparing REST-API Access

<img width="813" alt="image" src="https://user-images.githubusercontent.com/7360266/116474404-bc158180-a878-11eb-8368-729a863c06bc.png">

In your user-settings (https://github.com/settings/tokens/new), you'll have to create a Personal Access Token. This is the easiest way to access your APIs later on.


### Implementation
- You will need: 
- The `HttpClient`-class which can be created by using its constructor. It is used for making Http-Requests.
  - `DefaultRequestHeaders.Add` can be used to accept default headers that you want all your requests to have.
  - `Dispose` needs to be called when you are done using the `HttpClient`.
  - `Send` and `SendAsync` can be used to send an `HttpRequestMessage` and receive a `HttpResponseMessage`.
- The `HttpRequestMessage`-class can be created by using its constructor
  - The `HttpMethod`-argument defines the HTTP-Method that you are calling. We will mostly, or exclusively use `HttpMethod.Get`
  - The `requestUri`-argument needs to point at the REST API's endpoint.
  - The `Headers.Add`-Method can be used to add headers.
    - e.g. `request.Headets.Add("Content-Language", "se");` would add a header requesting Swedish Content-Language.
  - The `Content`-Property can be used to assign a Body to your HTTP request.
    - The `StringContent`-class takes a string in its constructor and enables you to add a string as a HTTP request's body.
- The `HttpResponseMessage`-class contains all sorts of information that has been sent as a response.
  - `StatusCode` contains the HTTP-StatusCode, e.g. `200: OK`
  - `Headers` contains all HTTP-Headers as Key-Value-Pairs.
  - `response.Content.ReadAsStream()` can be used to receive a stream for the HTTP-Body of the response.
- The `StreamReader`-class has a constructor that you need to pass a `Stream` into.
  - `ReadToEnd` allows you to read a full string from the stream.
- `JsonSerializer.Deserialize<T>(string jsonText)` Can convert a `string` to a C#-class of type `T` for you.
  - It requires, that you create a class that matches the response structure.
  - All fields that are returned should exist as a public property with getter and setter.
  - e.g.: Response: `{"name":"Marc Zaku", "job": "Teacher"}` 
  -       Class: `public class UserResponse{public string name{get;set;} public string job {get; set;}}
  -       Use: `var userResponse = JsonSerializer.Deserialize<UserResponse>(responseJsonText);`

So, what is our GitHub Explorer supposed to do?
- Ask the User for a User Name that he'd like to explore.
- Send a HTTP Request to `https://api.github.com/users/{username}` (replace {username} with the user input).
- You can read on how to authenticate over REST API over here: https://docs.github.com/en/rest/guides/getting-started-with-the-rest-api
  - In short: you will need a Header with the key `Authorization` and the value `token {yourtoken}`
  - You want to be authenticated with all Requests. Consider using DefaultRequestHeaders :)
- You can read on the API over here: https://docs.github.com/en/rest/reference/users#get-a-user
- The Response-Object is defined there as well.
- Among others, it contains the fields `name` and `company` which you are able to parse using the JSON-Parse.
  - As a reference, check out above sample on `JsonSerializer`.
- You will also find all other APIs documented over there :)


### Suggested Features:
#### Social Features:
These features should allow you to inspect your own as well as other's user's GitHub profiles, view their repositories (with a few stats on their repositories), as well as their oganizations. And look at an organization's members, to view their profiles and repositories and so forth. They teach you how to fetch and visualize information and how data is linked in REST APIs.
- Showing a User's Profile
- Listing a User's Repositories
- Listing a User's Organizations
  - Listing an Organization's Members (make them visitable)
  - Listing a User's Repositories

#### Issues & Commenting:
These features should allow you to create, inspect and close an issue and to view an issue's comments and add, update and delete them. They teach you how different HTTP-Methods are used for getting, creating, updating and deleting data on a REST API.
- Listing a Repository's Issues
- Creating an Issue
- Closing an Issue (PATCH)
    - Listing all Comments on an Issue
    - Commenting on an Issue
    - Deleting a Comment on an Issue
    - Editing a Comment on an Issue


### Bonus:
Develop a proper interface for all of your interactions with GitHub's Rest API.
Do not make any HTTP-Requests outside of those interfaces.
Imagine something like this:

```cs
public interface IGitHubAPI {
   List<Issue> GetIssues(string userName, string repositoryName);
   Issue CreateIssue(string userName, string repositoryName, string title, string description);
}

void Main(){
   IGitHubAPI gitHubAPI = new GitHubAPI();
   var issues = gitHubAPI.GetIssues("marczaku","GP20-2021-0426-Rest-Gameserver");
}
```

Now, take your API even one step further. 
Wrap all your API's return values into objects that do not only contain data.
But also methods to call the RESTful API.
Imagine something like this:

```cs
public interface IGitHubAPI {
   IUser GetUser(string userName);
}

public interface IUser {
   IRepository GetRepository(string repositoryName);
   string Name {get;}
   string Location {get;}
}

public interface IRepository {
   List<IIssue> GetIssues();
   string Name {get;}
   string Description {get;}
}

void Main(){
   IGitHubAPI gitHubAPI = new GitHubAPI();
   var user = gitHubAPI.GetUser("marczaku");
   var repository = user.GetRepository("GP20-2021-0426-Rest-Gameserver");
   var issues = repository.GetIssues();
}
```

Do you see the differences and improvements in these implementations?
Do you only see Advantages, or are there also disadvantages?



## Part 3: Lame-Scooter
---

## Goal
To implement your own CLI to read information about available Scooters at different stations. Implement several possible sources of information.

---

## Preparing a Project
Create a folder named `LameScooter`\
Open the Terminal in that Folder
Now, use the command `dotnet new console`\
Add a `.gitignore` in your `LameScooter`-Folder that ignores anything you don't want to commit.\
For C# Console Projects, that's at least the `/bin/` and `/obj/`-Folders.\
Afterwards, you may safely go ahead and create a new commit `adds lame scooter project`

<img width="564" alt="image" src="https://user-images.githubusercontent.com/7360266/117221072-d4514780-ae08-11eb-91e9-cfc6a680c4d8.png">

---

## Implementation

### Required Methods

- `JsonSerializer.Deserialize<T>(string jsonText, JsonSerializerOptions options)` Can convert a `string` to a C#-class of type `T` for you.
  - I recommend this time, to pass `JsonSerializerOptions` and set
    - `PropertyNamingPolicy = JsonNamingPolicy.CamelCase`
    - This enables you to name Properties with UpperCase-First, even, if the JSON-Field starts with LowerCase.
    - e.g.: `public string Name{get; set;}` vs. `{name: "Marc Zaku}"`
  - It requires, that you create a class that matches the response structure.
  - All fields that are returned should exist as a public property with getter and setter.
  - e.g.: Response: `{"name":"Marc Zaku", "job": "Teacher"}` 
  - class: `public class UserResponse{public string Name{get;set;} public string Job {get; set;}}`
  - use: `var userResponse = JsonSerializer.Deserialize<UserResponse>(responseJsonText, jsonSerializerOptions);`
- The `HttpClient`-class which can be created by using its constructor. It is used for making Http-Requests.
  - `DefaultRequestHeaders.Add` can be used to accept default headers that you want all your requests to have.
  - `Dispose` needs to be called when you are done using the `HttpClient`.
  - `Send` and `SendAsync` can be used to send an `HttpRequestMessage` and receive a `HttpResponseMessage`.
- The `HttpRequestMessage`-class can be created by using its constructor
  - The `HttpMethod`-argument defines the HTTP-Method that you are calling. We will mostly, or exclusively use `HttpMethod.Get`
  - The `requestUri`-argument needs to point at the REST API's endpoint.
  - The `Headers.Add`-Method can be used to add headers.
    - e.g. `request.Headets.Add("Content-Language", "se");` would add a header requesting Swedish Content-Language.
  - The `Content`-Property can be used to assign a Body to your HTTP request.
    - The `StringContent`-class takes a string in its constructor and enables you to add a string as a HTTP request's body.
- The `HttpResponseMessage`-class contains all sorts of information that has been sent as a response.
  - `StatusCode` contains the HTTP-StatusCode, e.g. `200: OK`
  - `Headers` contains all HTTP-Headers as Key-Value-Pairs.
  - `response.Content.ReadAsStream()` can be used to receive a stream for the HTTP-Body of the response.
- The `StreamReader`-class has a constructor that you need to pass a `Stream` into.
  - `ReadToEnd` allows you to read a full string from the stream.

### Routines

- Read the requested station name from the command line arguments.
- Load the [scooters.json-File](https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json)
- Look at the Format of the JSON-Response. Create classes that resemble the structure of the response.
- Use the `JsonSerializer.Deserialize<T>`-Method to deserialize the JSON-`string` to a C# object.
- Search the stations for a station with a matching name.
- Return the number of available scooters at this station.
- Print the result to the console: `Number of Scooters Available at this Station: 1234`

---

## Step By Step:

---

### 1. Print Console Arguments and Test the Output:

Change the Main Method to this:

```cs
static async Task Main(string[] args)
{
    Console.WriteLine(args[0]);
}
```

It will print the first command line argument passed to the application to the console. Try running the following commands in the Terminal:
- `dotnet run Hello`
- `dotnet run Hello-World`
- `dotnet run Hello World`
- `dotnet run Hello\ World`
- `dotnet run "Hello World" Planet`

What have you learned?

What happens, if you try Debugging your Application?

If you want to debug your application with arguments, follow the steps on the [Rider Documentation](https://www.jetbrains.com/help/rider/Get_Started_with_Run_Debug_Configurations.html). To sum things up: On the Top right, click on your configuration (the dropdown with your Project Name), click on Edit Configuration and put the arguments that you want to pass into the `Program Arguments`-Field. Are you able to print "Hello World" this way?

---

### 2. Create an Interface

In order to be able to easily exchange our Database-Implementations later, create the following interface:

```cs
public interface I.Rental
{
    Task<int> GetScooterCountInStation(string stationName);
}
```

And use the interface in your Main-Method:

```cs
static async Task Main(string[] args)
{
    ILameScooterRental rental = null; // Replace with new XXX() later.
    // The await keyword makes sure that we wait for the Task to complete.
    // and makes the result of the task available. Task<int> => int.
    var count = await rental.GetScooterCountInStation(null); // Replace with command line argument.
    Console.WriteLine("Number of Scooters Available at this Station: "); // Add the count that is returned above to the output.
}
```

---

### 3. Create a class implementing the interface

To prepare this part, Download [scooters.json](https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json) from this repository and copy it into your LameScooter-Project, right next to `LameScooter.csproj` and `Program.cs`
Create a class named `OfflineLameScooterRental` which implements `ILameScooterRental` and loads the text form the file `scooters.json` and parses the text into a C# Object.
In order for this to work, you will have to make sure, that the json file gets copied to your built application as well. To do so, right-click the `scooters.json`-file in Rider and open its Properties. Change `Copy to Output Directory` to `Copy Always`.
Now, when you have parsed the information into a C#-Object, find the right station for the given name, and return the amount of bikes available at this station.

More Details:
- Remember to use `async` and `await` keywords in your implementation of asynchronous methods.
- Next, you need to define your own `LameScooterStationList`-class that corresponds to the `scooters.json`-contents. It should contain Properties with same Names and Types as found in the Json.
- Deserialize the string to a C# object using `JsonSerializer.Deserialize<LameScooterStationList>`
- Find the information you are looking for from the `LameScooterStationList` (scooter count in a certain station) and return it as the result from the method

Samples:
- `dotnet run Linnanmäki`
- `dotnet run Sepänkatu`
- `dotnet run Pohjolankatu`

---

### 4. Handle Argument Errors

Throw an `ArgumentException` (provided in `System`) if the user calls `GetScooterCountInStation` with a string which contains numbers.
Catch the exception in the calling code (your Main Method) and print "Invalid Argument: " and the `Message`-Property of the exception.

---

### 5. Create and throw your own Exception
Create your own `Exception` called `NotFoundException`. Throw it, if the station can not be found. 
Catch it in the calling code and print "Could not find: " and the Message Property of the exception.

---

### 6. Create an alternative implementation
Create a class called `DeprecatedLameScooterRental` which also implements the `ILameScooterRental`-Interface. Download [scooters.json](https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.txt) and also put it into your project and again make sure, that it gets copied when Building. Find a way to read the required data from the file.

How much code can you share? How much code do you have to duplicate?

Samples:
- `dotnet run Linnanmäki`
- `dotnet run Sepänkatu`
- `dotnet run Pohjolankatu`

---

### 7. Implement more Command Line Arguments
Make the console application accept an additional, optional string argument, `offline` or `deprecated` and decide the implementation of `ILameScooterRental` based on that.

Samples:
- `dotnet run Linnanmäki offline`
- `dotnet run Sepänkatu deprecated`
- `dotnet run Pohjolankatu offline`

---

### 8. RealTime-Bike-Data
Create a class called `RealTimeLameScooterRental` which also implements the `ILameScooterRental`-Interface. Make it load the BikeData using a HTTP-GET-Request on the URL `https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json` and use the result of that request. Use the argument `realtime` to decide the implementation of `ILameScooterRental` with this new class.

Samples:
- `dotnet run Linnanmäki realtime`
- `dotnet run Sepänkatu realtime`
- `dotnet run Pohjolankatu realtime`

---

### BONUS: 9. MongoDB Database

Documentation of MongoDB Server:
- [Install MongoDB](https://docs.mongodb.com/manual/installation/)
- [Getting Started](https://docs.mongodb.com/manual/tutorial/getting-started/)

Documentation of MongoDB .NET:
- [Install MongoDB.Driver](http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/installation/)
- [Quick Tour](http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/quick_tour/)

Create a class called `MongoDBLameScooterRental` which also implements the `ILameScooterRental`-Interface. Make it load the BikeData from a Mongo-Collection named `lamescooters`. Use the command line argument `mongodb` to decide the implementation of `ILameScooterRental` with this new class.

You need to create and populate the collection first.
Open the Terminal and connect to your local MongoDB-Server.
If you haven't yet, follow your OS' `Install MongoDB`-Tutorial and make sure to also launch the Server.
- In the Terminal, enter `mongo` to connect to your Database.
- Use `db` to see the current database's name.
- Use `use lamescooters` to switch to / create a collection named lamescooters.
- Use `db.lamescooters.insertMany([.......]);` but replace `[.....]` with the part that you find in the `scooters.json`-file. Make sure, to select the WHOLE CONTENT between `[` and `]`.
- Try querying a station by using `db.lamescooters.find({"name":"Lammasrinne"});`. It should return exactly one station with that name.

Add MongoDB.Driver as a package to your Project, so you can use MongoDB in C#:
`dotnet add package MongoDB.Driver`

Now, follow the QuickTour of the Mongo C# Driver to find out, how to query a station from that database and try to return the available bike count.

Samples:
- `dotnet run Linnanmäki mongodb`
- `dotnet run Sepänkatu mongodb`
- `dotnet run Pohjolankatu mongodb`

---

### 10. GOOD JOB!

You have completed the whole introduction course! Part 4 will contain the actual assignment. But I've figured, that it'll do, if I hand out that part in the beginning 

# Part 4 Pre-Production: MMO-RPG (Mass Multiplayer REST Playing Game)

All of these Tasks are completely optional and not graded, but very recommended, as the actual game will be based on these exercises.

---

## Goal
To implement your own REST-Server using ASP .NET Core Webserver-Technology combined with MongoDB as a database. Learn to CRUD documents.

---

## Preparing a Project
- Create a folder named `MMORPG`
- Open the Terminal in that Folder
- Now, use the command `dotnet new webapi`
- Add a `.gitignore` in your `MMORPG`-Folder that ignores anything you don't want to commit.
- For C# Console Projects, that's at least the `/bin/` and `/obj/`-Folders.
- Go to the newly created `Startup.cs` class. Remove the line that says `app.UseHttpsRedirection();`. This will make testing easier for now.
- Afterwards, you may safely go ahead and create a new commit `adds mmorpg project`

---

## 1. Create Model classes

Create the following classes in separate files inside the project folder:

`Player` class is used to save the player's save data.

```cs
public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreationTime { get; set; }
}
```

`NewPlayer` class is used to define object that contains the properties that are defined by the client when creating new player. `Id` and `CreationDate` should be set by the server when the player is created.

```cs
public class NewPlayer
{
    public string Name { get; set; }
}
```

`ModifiedPlayer` class contains the properties that can be modified on a player.

```cs
public class ModifiedPlayer
{
    public int Score { get; set; }
}
```

---


## 2. Create a FileRepository class and IRepository interface

The responsibility of the `Repository` is to handle accessing and persisting player objects.

Create the following interface in a new file:

```cs
public interface IRepository
{
    Task<Player> Get(Guid id);
    Task<Player[]> GetAll();
    Task<Player> Create(Player player);
    Task<Player> Modify(Guid id, ModifiedPlayer player);
    Task<Player> Delete(Guid id);
}
```

Create a class called `FileRepository` which implements the interface. The purpose of the class is to persist and manipulate the `Player` objects in a text file. One possible solution is to serialize the players as JSON to the text file. The text file name should be `game-dev.txt`. You can use, for example, `File.ReadAllTextAsync` and `File.WriteAllTextAsync` methods for the implementation.

Usually, you'd have to worry about asynchronous access to that file (what happens, if two players connect to your web server at the same time?).  But we'll skip on this for now.

---

## 3. Create a PlayersController class

The first responsibility of the controller is to define the routes for the API. Define the routes using attributes. There is `[HttpPost]`, `[HttpPut]` and a few more.

The second responsibility is to handle the business logic. Business logic is a term for the core of your application. Everything that creates transactions that change your data / model. This can include things such as generating IDs when creating a player, and deciding which properties to change when modifying a player.

Create a class called `PlayersController`. Add and implement the following methods:

`PlayersController` should get `IRepository` through its constructor. We will learn later, how we can provide this information through Dependency Injection.

```C#
public Task<Player> Get(Guid id);
public Task<Player[]> GetAll();
public Task<Player> Create(NewPlayer player);
public Task<Player> Modify(Guid id, ModifiedPlayer player);
public Task<Player> Delete(Guid id);
```

---

## 4. Register IRepository to DI-container

Register `FileRepository` to the DI-container in `Startup.ConfigureServices()`-Method.

Registering the `FileRepository` as `IRepository` into the dependency injection container enables changing the implementation later on when we start using `MongoDB` as the database.

---

## 5. Test

Use either [PostMan](https://learning.postman.com/docs/sending-requests/requests/) or your own .NET-Code to test that the requests to all routes are processed successfully.\
How about adding a Console-Project to the solution for testing some Client-Code using `HttpClient`?

---

## 6. Bonus: Automated Tests

Can you use wither [Automated Tests in PostMan](https://learning.postman.com/docs/writing-scripts/test-scripts/) or your own Unit-Test-Project to validate that all end points work as expected?

---

## 7. Implement CRUD routes and data classes for `Item`

The idea is that items are always owned by players (there is no item not owned by a player).\
Define the Endpoints for Creating, Reading, Updating and Deleting an Item.

- Extend the `IRepository`-Interface and `FileRepository`-Class.
- Add an `Item`-Class and all other classes needed for the operations.
  - Use the `Player`-Classes as a reference.
- Add an `ItemsController` and define the endpoints for `Item`-CRUD-Operations
  - All item routes should start with `players/{playerId}/items`.

---

## 7. MongoDb

Now it's time to use a real MongoDb database. Create a class `MongoDbRepository` which has the responsibility to do everything that is related to accessing data in MongoDb. It will replace your existing `FileRepository`.

`MongoDbRepository` should also implement the `IRepository` interface - just like the `FileRepository` does.

When it's time to run your application with MongoDb, remember to replace the `FileRepository` registration with the new `MongoDbRepository` in the DI-Container! (in `Startup.cs`)

Your data should follow this format:

- You can name your database to `game`
- Players should be stored in a collection called `players`
- `Items` should be stored in a list inside the `Player` document

---

## 8. Error handling

Create your own middleware for handling errors called `ErrorHandlingMiddleware`.

Create your own exception class called `NotFoundException`.

Throw the `NotFoundException` when `Player` is not found (incorrect {playerId} passed in the route) when trying to add new `Item`.

Catch the `NotFoundException` in the `ErrorHandlingMiddleware`. And then on the catch block: set the HTTP status code to 404 (not found) to the `HttpContext` that is passed to the middleware.

---

## 9. Model validation using attributes

`NewItem` and `Item` models should have the following properties:

- int Level
- ItemType Type (define the `ItemType` enum yourself with values SWORD, POTION and SHIELD)
- DateTime CreationDate

Define the following validations for the model using attributes:

- `Level` can be only within the range from 1 to 99
- `Type` is one of the types defined in the `ItemType` enum
- `CreationDate` is a date from the past (Create custom validation attribute)

---

## 10. Implement a game rule validation in Controller

This is an extra exercise. You will get bonus points for completing this.

Implement a game rule validation for the `POST`-Route (the one that creates a new item) in the `ItemsController`:

The rule should be: an item of type of `Sword` should not be allowed for a `Player` below level 3.

If the rule is not followed, throw your own custom exception (create the exception class) and catch the exception in an `exception filter`. The `exception filter` should write a response to the client with a _suitable error code_ and a _descriptive error message_. The `exception filter` should be only applied to that specific route.

---

## 11. Queries

### 1. Ranges

Get `Players` who have more than x score

**hints:**

Specify the x in the query like this: `...api/players?minScore=x`

### 2. Selector matching

Get `Player` with a specified name

**hints:**

Make sure the API can handle routes `...api/players/{name}` _and_ `..api/players/{id}`

You can use attribute constraints or use a query parameter like this: `...api/players?name={name}`

### 3. Set operators

Add `Tags` for the `Player` model ( `Tags` can be a list of strings) and create a query that returns the `Players` that have a certain tag.

### 4. Sub documents queries

Find `Players` who have `Item` with certain property

### 5. Size

Get `Players` with certain amount of `Items` by using size method

### 6. Update

Update `Player` name without fetching the `Player`

### 7. Increment

Increment `Player` score without fetching the `Player`

### 8. Push

Add `Item` to the item list property on the `Player`

### 9. Pop and increment as an atomic operation

Remove from `Item` from `Player` and add some score for the `Player`. You can think of this as a `Player` selling an `Item` and getting score as a reward.

**hints:**

The route should be `..api/players/{playerId}/items/` with DELETE Http verb.

### 10. Sorting

Get top 10 `Players` by score in descending order

---

## 12. Aggregation

### 1. Aggregation exercise based on the example in the slides

Find out what is the most common level for a player (example in the slides).

### 2. Intermediate aggregation exercise

Get the item counts for different prices for items.

### 3. Difficult aggregation exercise

Get the average score for players who were created between two dates using aggregation.

# Part 4: MMO-RPG (Mass Multiplayer REST Playing Game) (70 Points)

All of these Tasks are completely optional but very recommended, as the actual game will be based on these exercises.

---

## Goal
To implement your own Game Client & REST-Server using ASP .NET Core Webserver-Technology combined with MongoDB as a database and either a Console or Unity Application as a Client.\
I do not care about how exactly the player interacts with the game / backend. But it is important, that:
- All data is stored in MongoDB
- All operations are available through a REST-API implemented in ASP .NET Core
  - The Rest Endpoints are following REST Standards (CRUD, HTTP-Methods, Query-Syntax, ...)
- Every Feature is accessible through a Game Client.
  - Suggestion: Build a Console Client (similar to GitHub Browser) that asks you what you want to do next. 1: See Quests 2: See Items 3: See Leaderboard etc.
  - Alternatively: Build a Unity-Client, where you visualize everything with UI Elements. Warning: This probably takes much more time!


---

## Feature-List

### Players
- Players can create and delete their players.
- They can find other players by their names.
- They can access their player through their id.
- They should not be able to see other players' ids.

### Quests
- Players can see and do quests to receive rewards.
- Players get a new random quest every minute.
- They can have up to 5 different quests at a time.
- Any new Quest then removes / invalidates the previous Quest.
- A quest has a Level Requirement and a Reward.
- A player can only do a quest, if he fulfils the Level Requirement.
- It grants Experience to the player.
- It grants the Reward to the player.
- A Reward can be 1-X Gold, or a Random Item.

### Level
- Players start with 0 Levels and 0 Experience.
- Every level requires N*100 Experience to level-up.
  - e.g: Level 0 -> Level 1 (100XP)  Level 1 -> Level 2 (200XP)
- Experience is always capped at the Level's Requirement.
- In order to level up, the player needs to purchase a Level-Up. It costs N*100 Gold.
  - e.g: Level 0 -> Level 1 (100 Gold)  Level 1 -> Level 2 (200 Gold)

### Gold
- Gold can be a Reward through a Quest.
- It can be spent for Level-Ups.
- Through Level-Ups, the Player is able to fulfil Quests with higher Level Requirements.

### Item
- Items can be Quest Rewards.
- They have Rarity, Type, Price, Level Requirement, Level Bonus.
  - Rarity: Common, Uncommon, Rare, Epic
  - Type: Weapon, Armor, Helmet
- An Item can be Sold to receive its Price in Gold.
- It can be Equipped, if the Player fulfils the Level Requirement.
- While Equipped, the Item's Level Bonus is Added to the Player's Level.
- The player can only have one Item of each Kind Equipped at the same Time.
- If the player sells an equipped item, it is not Equipped anymore.
- An Item's Level Bonus also helps with Equipping Items with high Level Requirements.
  - e.g: A Level 2 Player an Equip a Helmet with 1 Level Bonus.
  - which enables him to equip a Sword with a Level 3 Requirement.

### Leaderboard
- Players can see the top 10 players by level.
- And by Gold.

### Statistics
- Players can see the total count of players.
- And the total amount of Gold owned by all players.
- And the total amount of Items owned by all players.
- And the total amount of Levels of all players.
- Players can inspect other players.
  - They can only see their equipped items.
  - And they can see the Level described as:
    - Difference in Level smaller than 3 ? "EQUAL"
    - Player 3 or more levels higher? "STRONGER"
    - Player 3 or more levels lower? "WEAKER"
  - And yes, if players are in the Top 10 Leaderboard, their information is a little more public :-)
