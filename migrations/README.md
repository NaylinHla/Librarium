# Librarium

## Setup

Requirements:
- .NET 8 SDK
- PostgreSQL

Update the database from the repository root:
dotnet ef database update --project src/Librarium/Librarium.Data/Librarium.Data.csproj --startup-project src/Librarium/Librarium.Api/Librarium.Api.csproj

Start the API:
dotnet run --project src/Librarium/Librarium.Api/Librarium.Api.csproj

The API will start and expose the endpoints.

# Database Migrations

Versioned SQL migrations for the Librarium database.

## V001__initial_schema.sql
Date: 2026-03-09

**Description**  
Creates the initial schema with the Books, Members, and Loans tables.

**Type of change**  
Additive (non-breaking)

**API impact**  
No API endpoints existed before this migration.

**Deployment notes**  
Must be applied before the API can run.

**Decisions and tradeoffs**  
This migration establishes the core entities and their relationships.
Loans reference both Books and Members through foreign keys. The schema
was intentionally kept simple so later requirements could evolve the
structure through additional migrations.

---

## V002__add_authors_to_books.sql
Date: 2026-03-09

**Description**  
Adds Authors and BookAuthors tables so books can have multiple authors.

**Type of change**  
Additive (non-breaking)

**API impact**  
`GET /api/books` now includes authors. This is backwards compatible since
clients can ignore the new field.

**Deployment notes**  
Safe to deploy before or after the application update.

**Decisions and tradeoffs**  
Existing books already existed without authors. Making the relationship
required would have caused migration issues, so it was implemented as
optional. An endpoint was added to attach authors later if needed.

---

## V003__add_nullable_phone_number_to_members.sql
Date: 2026-03-09

**Description**  
Adds a nullable `PhoneNumber` column to Members.

**Type of change**  
Additive (potentially breaking later)

**API impact**  
No API changes yet. The column prepares the schema for new registration
requirements.

**Deployment notes**  
Safe to deploy before updating the application because the column is
nullable.

**Decisions and tradeoffs**  
Existing members do not have phone numbers. Introducing the column as
nullable allows the system to transition without breaking current data.
Duplicate emails and missing phone numbers must be resolved before the
next enforcement migration.

---

## V004__enforce_unique_email_and_required_phone_number.sql
Date: 2026-03-09

**Description**  
Makes `PhoneNumber` required and enforces uniqueness on `Email`.

**Type of change**  
Additive (potentially breaking)

**API impact**  
Member creation now requires a phone number and duplicate emails are
rejected.

**Deployment notes**  
Duplicate emails and missing phone numbers must be resolved before this
migration runs.

**Decisions and tradeoffs**  
The constraints were introduced in a separate migration so existing data
could be cleaned first. This avoids migration failures and allows the
application to transition safely to the new requirements.

---

## V005__add_loan_status.sql
Date: 2026-03-09

**Description**  
Adds a `Status` column to Loans.

**Type of change**  
Additive (non-breaking)

**API impact**  
The existing loan endpoint still uses `ReturnDate` to determine whether
a loan is open.

**Deployment notes**  
Safe to deploy before the application update because the column has a
default value.

**Decisions and tradeoffs**  
The frontend cannot update immediately, so the API contract must stay
unchanged. Existing loans are backfilled based on `ReturnDate` while the
new `Status` field enables more detailed loan states in the future.

---

## V006__add_book_retirement.sql
Date: 2026-03-09

**Description**  
Adds an `IsRetired` flag to Books.

**Type of change**  
Additive (non-breaking)

**API impact**  
`GET /api/books` now excludes retired books, but loan history still
returns book data.

**Deployment notes**  
Safe to deploy before or after the application update.

**Decisions and tradeoffs**  
The original proposal used `IsDeleted`. This was changed to `IsRetired`
to better reflect the business meaning. Retired books are hidden from
catalog searches and blocked from new loans, but remain available in
loan history for auditing.

---

## Requirement 5 note — ISBN column

**Description**  
No migration was required for ISBN.

**Type of change**  
None

**API impact**  
No API changes were required.

**Deployment notes**  
No deployment considerations were needed.

**Decisions and tradeoffs**  
The requirement assumes ISBN was stored as an integer. In this
implementation it was already defined as a string, so the schema already
supports formatted ISBN values and no migration was necessary.