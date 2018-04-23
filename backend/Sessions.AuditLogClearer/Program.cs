using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sessions.AuditLogClearer.Database;
using Sessions.Models;

namespace Sessions.AuditLogClearer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AuditLogCleanerContext())
            {
                var oldReadLogs = context.AuditLogs
                    .OrderBy(log => log.Date)
                    .Where(log => log.Action == Actions.Read && log.Date < DateTime.Now.AddHours(-1))
                    .ToList();
                if (oldReadLogs.Any())
                {
                    Console.WriteLine($"Removing {oldReadLogs.Count} READ logs...");
                    context.AuditLogs.RemoveRange(oldReadLogs);
                    context.SaveChanges();
                }
            }
        }
    }
}
