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