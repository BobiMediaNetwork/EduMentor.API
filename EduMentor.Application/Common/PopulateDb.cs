using Bogus;
using EduMentor.Application.Common.DTOs;
using EduMentor.Domain.Model;
using System.Text.RegularExpressions;

namespace EduMentor.Application.Common;

public static class PopulateDb
{
    private const int NumberOfStudents = 10;

    public static PopulateDto populateDto = new()
    {
        AdminId = Guid.NewGuid(),
        StudentId = Guid.NewGuid(),
        ParentId = Guid.NewGuid(),
        TeacherId = Guid.NewGuid(),
        DirectorId = Guid.NewGuid()
    };

    public static string Populate()
    {
        var faker = new Faker();
        var migrationString = string.Empty;
        Utils.CreatePasswordHash("password", out var hash, out var salt);
        var passwordHash = Convert.ToBase64String(hash);
        var passwordSalt = Convert.ToBase64String(salt);

        // Admin
        var userAdmin = new User
        {
            Id = Guid.NewGuid(),
            FirstName = CleanString(faker.Person.FirstName),
            LastName = CleanString(faker.Person.LastName),
            Username = faker.Person.UserName.ToLower(),
            Email = faker.Person.Email.ToLower(),
            RoleId = populateDto.AdminId,
            DateOfBirth = DateOnly.FromDateTime(faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2005, 1, 1)))
        };

        migrationString +=
            $"INSERT INTO [Users] " +
            $"VALUES ('{userAdmin.Id}', '{userAdmin.FirstName}', '{userAdmin.LastName}', '{userAdmin.Email}', '{userAdmin.Username}', " +
            $"'{userAdmin.DateOfBirth.ToString("yyyy-MM-dd")}', " +
            $"CONVERT(varbinary(max), CAST('{passwordHash}' AS XML).value('.', 'VARBINARY(MAX)')), " +
            $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
            $"'{userAdmin.RoleId}', 0)\n";


        // Director
        faker = new Faker();
        var userDirector = new User
        {
            Id = Guid.NewGuid(),
            FirstName = CleanString(faker.Person.FirstName),
            LastName = CleanString(faker.Person.LastName),
            Username = faker.Person.UserName.ToLower(),
            Email = faker.Person.Email.ToLower(),
            RoleId = populateDto.DirectorId,
            DateOfBirth = DateOnly.FromDateTime(faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2005, 1, 1)))
        };

        migrationString +=
            $"INSERT INTO [Users] " +
            $"VALUES ('{userDirector.Id}', '{userDirector.FirstName}', '{userDirector.LastName}', '{userDirector.Email}', '{userDirector.Username}', " +
            $"'{userDirector.DateOfBirth.ToString("yyyy-MM-dd")}', " +
            $"CONVERT(varbinary(max), CAST('{passwordHash}' AS XML).value('.', 'VARBINARY(MAX)')), " +
            $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
            $"'{userDirector.RoleId}', 0)\n";

        // Students
        var students = new List<User>();
        for (var i = 0; i < NumberOfStudents; i++)
        {
            faker = new Faker();
            var userstudent = new User
            {
                Id = Guid.NewGuid(),
                FirstName = CleanString(faker.Person.FirstName),
                LastName = CleanString(faker.Person.LastName),
                Username = faker.Person.UserName.ToLower(),
                Email = faker.Person.Email.ToLower(),
                RoleId = populateDto.StudentId,
                DateOfBirth = DateOnly.FromDateTime(faker.Date.Between(new DateTime(2010, 1, 1), new DateTime(2015, 1, 1)))
            };

            migrationString +=
                $"INSERT INTO [Users] " +
                $"VALUES ('{userstudent.Id}', '{userstudent.FirstName}', '{userstudent.LastName}', '{userstudent.Email}', '{userstudent.Username}', " +
                $"'{userstudent.DateOfBirth.ToString("yyyy-MM-dd")}', " +
                $"CONVERT(varbinary(max), CAST('{passwordHash}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"'{userstudent.RoleId}', 0)\n";

            students.Add(userstudent);
        }

        // Teachers

        var teachers = new List<User>();
        for (var i = 0; i < 14; i++)
        {
            faker = new Faker();
            var userTeacher = new User
            {
                Id = Guid.NewGuid(),
                FirstName = CleanString(faker.Person.FirstName),
                LastName = CleanString(faker.Person.LastName),
                Username = faker.Person.UserName.ToLower(),
                Email = faker.Person.Email.ToLower(),
                RoleId = populateDto.TeacherId,
                DateOfBirth = DateOnly.FromDateTime(faker.Date.Between(new DateTime(1940, 1, 1), new DateTime(2006, 1, 1)))
            };

            migrationString +=
                $"INSERT INTO [Users] " +
                $"VALUES ('{userTeacher.Id}', '{userTeacher.FirstName}', '{userTeacher.LastName}', '{userTeacher.Email}', '{userTeacher.Username}', " +
                $"'{userTeacher.DateOfBirth.ToString("yyyy-MM-dd")}', " +
                $"CONVERT(varbinary(max), CAST('{passwordHash}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"'{userTeacher.RoleId}', 0)\n";

            teachers.Add(userTeacher);
        }

        // Parents

        var parents = new List<User>();
        for (var i = 0; i < NumberOfStudents; i++)
        {
            faker = new Faker();
            var userParent = new User
            {
                Id = Guid.NewGuid(),
                FirstName = CleanString(faker.Person.FirstName),
                LastName = CleanString(faker.Person.LastName),
                Username = faker.Person.UserName.ToLower(),
                Email = faker.Person.Email.ToLower(),
                RoleId = populateDto.ParentId,
                DateOfBirth = DateOnly.FromDateTime(faker.Date.Between(new DateTime(1940, 1, 1), new DateTime(1995, 1, 1)))
            };

            migrationString +=
                $"INSERT INTO [Users] " +
                $"VALUES ('{userParent.Id}', '{userParent.FirstName}', '{userParent.LastName}', '{userParent.Email}', '{userParent.Username}', " +
                $"'{userParent.DateOfBirth.ToString("yyyy-MM-dd")}', " +
                $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"CONVERT(varbinary(max), CAST('{passwordSalt}' AS XML).value('.', 'VARBINARY(MAX)')), " +
                $"'{userParent.RoleId}', 0)\n";

            parents.Add(userParent);
        }

        return migrationString;
    }

    private static string CleanString(string input)
    {
        return Regex.Replace(input, @"[^a-zA-Z0-9\s]", "");
    }
}
