# Database Migrations

Versioned SQL migrations for the Librarium database.

## V001__initial_schema.sql
Date: 2026-03-09

Initial database schema.

Creates tables:
- Books
- Members
- Loans

Relationships:
- Loans.BookId → Books.Id
- Loans.MemberId → Members.Id

## V002__add_authors_to_books.sql
Date: 2026-03-09

Adds Authors and BookAuthors tables.

Books may exist without authors. Endpoint created to add an Author later if wanted.

## V003__add_nullable_phone_number_to_members.sql
Date: 2026-03-09

Adds PhoneNumber to Members as nullable to allow staged rollout.

I would while this is in production decide how to handle duplicate emails and users without phone numbers.

## V004__enforce_unique_email_and_required_phone_number.sql
Date: 2026-03-09

Makes PhoneNumber required in Members and enforces uniqueness on Email.

This migration assumes duplicate emails have been resolved and missing phone numbers have been populated before execution.

## V005__add_loan_status.sql
Date: 2026-03-09

Adds Status to Loans with a default value of Active.

Existing loans are backfilled based on ReturnDate:
- ReturnDate is null -> Active
- ReturnDate is not null -> Returned

The existing loan API contract remains unchanged so current clients can continue to use ReturnDate during until frontend team is ready >:D

## V006__add_book_retirement.sql
Date: 2026-03-09

Adds IsRetired to Books with a default value of false.

Review note:
Accepted the soft-delete idea but changed the implementation.
Renamed IsDeleted to IsRetired to better match the business meaning.
Retired books are hidden from catalogue/search and cannot receive new loans, but they remain visible in loan history so existing records still resolve their book details.
Avoided a global filter to prevent book data from disappearing in loan responses.