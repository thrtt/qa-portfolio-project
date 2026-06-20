# Restful Booker API Testing

This project contains API testing scenarios created with Postman for the Restful Booker API.

## API Under Test

Restful Booker API

Base URL:

https://restful-booker.herokuapp.com

## Scope of Testing

The following API functionalities are covered:

* Authentication
* Create Booking
* Get Booking IDs
* Get Booking by ID
* Update Booking
* Search Booking by First Name
* Search Booking by Last Name
* Delete Booking
* Verify Deleted Booking Returns 404
* Negative API Testing

## Positive Test Scenarios

* Generate authentication token
* Create a new booking
* Retrieve booking details by ID
* Update an existing booking
* Search bookings by first name
* Search bookings by last name
* Delete a booking
* Verify deleted booking is no longer available

## Negative Test Scenarios

* Authentication with invalid credentials
* Get booking with invalid ID
* Update booking without authentication
* Delete booking without authentication

## Environment Variables

The collection uses the following variables:

* `baseUrl`
* `bookingId`
* `token`

## Tools Used

* Postman
* Postman Environment Variables
* JavaScript Test Scripts

## Documentation

Swagger/OpenAPI documentation:

https://restful-booker.herokuapp.com/apidoc/index.html

## Status

Completed

## Notes

This project demonstrates API testing using Postman, including CRUD operations, authentication handling, environment variables, response validation, positive test scenarios, and negative test scenarios.
