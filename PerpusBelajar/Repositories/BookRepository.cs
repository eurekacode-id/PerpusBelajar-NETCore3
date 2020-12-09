using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerpusBelajar.Models;
using PerpusBelajar.Interfaces;
using PerpusBelajar.Constant;
using Microsoft.EntityFrameworkCore;

namespace PerpusBelajar.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> _bookList;
        private readonly PerpusBelajarDbContext context;

        public BookRepository(PerpusBelajarDbContext context)
        {
            this.context = context;
            _bookList = new List<Book>()
            {
                new Book(){Id=1,Title="Harry Potter and the Sorcerer's Stone",ISBN="0001",Author="JK Rowling",Quantity=5,ImageFileName="Book1.jpg",Category=BookCategory.Novel},
                new Book(){Id=2,Title="Harry Potter and the Chamber of Secret",ISBN="0002",Author="JK Rowling",Quantity=5,ImageFileName="Book2.jpg",Category=BookCategory.Novel}
            };
        }

        public Book GetBook(int id)
        {
            return context.Books.Find(id); ;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return context.Books;
        }

        public Book Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges(); 
            return book;
        }

        public Book Update(Book bookChanges)
        {
            var book = context.Books.Attach(bookChanges);
            book.State = EntityState.Modified;
            context.SaveChanges();
            return bookChanges;
        }

        public Book Delete(int id)
        {
            Book book = context.Books.Find(id);
            if(book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }

            return book;
        }


        #region MockBookRepo
        //public Book GetBook(int id)
        //{
        //    return _bookList.FirstOrDefault(e => e.Id == id);
        //}

        //public IEnumerable<Book> GetAllBooks()
        //{
        //    return _bookList;
        //}

        //public Book Create(Book book)
        //{
        //    book.Id = _bookList.Max(e => e.Id) + 1;
        //    _bookList.Add(book);
        //    return book;
        //}

        //public Book Delete(int id)
        //{
        //    Book book = _bookList.FirstOrDefault(e => e.Id == id);
        //    if(book != null)
        //    {
        //        _bookList.Remove(book);
        //    }

        //    return book;
        //}



        //public Book Update(Book bookChanges)
        //{
        //    Book book = _bookList.FirstOrDefault(e => e.Id == bookChanges.Id);
        //    if (book != null)
        //    {
        //        book.Title = bookChanges.Title;
        //        book.Author = bookChanges.Author;
        //        book.ISBN = bookChanges.ISBN;
        //        book.Quantity = bookChanges.Quantity;
        //        book.ImageFileName = bookChanges.ImageFileName;
        //        book.Category = bookChanges.Category;
        //    }

        //    return book;
        //}

        #endregion
    }
}
