// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Word_Puzzle.Data;
using Word_Puzzle.Model;


try
{
    List<Insert> inserts = new List<Insert>();

    List<string> allLines = File.ReadAllLines("word_list.txt").ToList();


    int count = 1;
    foreach (string line in allLines)
    {
        string[] wordDef = line.Split('-');
        if (wordDef.Length > 1)
        {
            AddToDb(wordDef, count);
            count++;
        }

    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadLine();
}

void AddToDb(string[] entry, int count)
{
    try
    {
        ApplicationDbContext db = new ApplicationDbContext();
        if (entry.Length > 1)
        {
            Guid wordGuid = Guid.NewGuid();
            // db.Definitions.Add(new Definition { ID = Guid.NewGuid(), Def = entry[1], WordID = wordGuid });
            List<Definition> defs = new List<Definition>();
            Word word = new Word {  Text = entry[0].ToLowerInvariant().Trim(), Definitions = defs };
            defs.Add(new Definition { Id = Guid.NewGuid(), Text = entry[1], Word = word });
           
            db.Words.Add(word);
            db.SaveChanges();
        }

        db.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadLine();
    }



}
public class Insert
{
    public Word word { get; set; }
    public Definition definition { get; set; }
}

internal class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Words;Trusted_Connection=True;TrustServerCertificate=true;");
    }

    public DbSet<Word> Words { get; set; }
    public DbSet<Definition> Definitions { get; set; }
}
