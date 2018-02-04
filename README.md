# Introduction
This repository contains the source code for SportsStore. SportsStore is a hello world implementatyion for DevOps. Please see 
[Roadmap To Agile DevOps Lab](http://roadmaptoagile.com/devops-lab-1) for details.

The infrastructure for SportsStore is implemented using the Infrastructure as Code approach. This repository is available [here](https://github.com/socamb/SportsStoreInfrastructure)
NOTE: I am currently working on a delivery pipeline using Team City, Octopus Debloy and Jira. 

# Getting Started
Please review the lab information in the above link to understand how this codebase is used in a DevOps Implementation.
This is a Microsoft MVC5 solution based on the book [Pro ASP.NET MVC 5](http://www.apress.com/us/book/9781430265290) It contains the following projects:
1.	SportsStore.Domain - implements the classes for the database ojbects in SportsStore.
2.	SportsStore.Tests - implements a set of test in the MSTest framework. This includes mocked unit tests, integration tests the hit the database and Selenimum QA tests.
3.	SportsStore.WebUi - implements the user interfact using MVC5.
4.	SportsStoreDB - impelemets the database using a database project.
5.  SportsStoreAutomation- implements a set of Selenium WebDrivers classes as an automation test framework.




