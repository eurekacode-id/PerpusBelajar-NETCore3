using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerpusBelajar.Models;

namespace PerpusBelajar.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        IEnumerable<Book> GetAllBooks();
        Book Create(Book book);
        Book Update(Book book);
        Book Delete(int id);
    }
}
