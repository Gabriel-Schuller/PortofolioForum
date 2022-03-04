# ForumAPI

## Story
This project is a .Net Core API project meant to offer the backend functionality for a forum. The project contains 4 repositories, for all CRUD operations for Questions, Answers, Comments and Users. Searching any of those by a word is also implemented, along with checkups for the questions/comments/answers to be unique. Users also have a jwt token functionality added to them.


## What are you going to learn?

>How to create a web API

>Work with Entity Framework

>Work with AutoMapper

>Work with Jwt token on the backend side

>How to send a Coockie from the backend

>Work with different routes for the controller

>Work with Swagger

>Work with CORS / create cors Policies that will make the results available on the frontend side.

>Work with repository pattern


## Tasks

1. Implement controllers for questions/answers
    - Controller will contain all CRUD operations
    - There will be cheeckups if the question/answer/comment is unique and nobody else wrote the specific question or part of it
    - All the controllers will implement search by id or word 
    - Calls t o the api with bad input will throw exceptions

2. Implement repository pattern
    - There will be a folder called Repository in the Service folder
    - This folder will implement the repository pattern for uses, questions, answers and comments and also a base repository for Adding, Deletion and Saving changes to 	the database
    - Repository will work in conjunction with Entity Framework

3. Use EntityFramework for creating a database and manage its entities
    - Project will use entity framework code first for creating tables for users. answers, questions and comments
    - There is a separation between entities/usermodels (can use AutoMapper)
    - Migrations will be used to keep track of changes 


## General requirements

None

## Pictures



![FileSeparation](Images/FileSeparation.PNG?raw=true "Title")

![Dependency Packages](Images/DependencyPackages.PNG?raw=true "Title")

