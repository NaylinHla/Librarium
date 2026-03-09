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