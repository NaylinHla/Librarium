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