﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Miedviediev_03.Exceptions;
using Miedviediev_03.ViewModels.PersonVm;

namespace Miedviediev_03.Models
{
    [Serializable]
    public class Person
    {
        private readonly string _name;
        private readonly string _surname;
        private readonly string _email;
        private DateTime _birthday;
        private readonly bool _isAdult;
        private readonly WesternZodiac _westernZodiac;
        private readonly ChineseZodiac _chineseSign;
        private readonly bool _isBirthday;

        public string Name => _name;

        public string Surname => _surname;

        public string Email => _email;

        public DateTime Birthday => _birthday;

        public bool IsAdult => _isAdult;
        
        public bool IsBirthday => _isBirthday;

        public WesternZodiac WesternZodiac => _westernZodiac;

        public ChineseZodiac ChineseSign => _chineseSign;
        

        public Person(string name, string surname, string email, DateTime birthday)
        {
            _birthday = birthday;
            (_name, _surname, _email, _westernZodiac, _chineseSign, _isAdult, _isBirthday) = 
                CreateObject(name, surname, email);
        }

        public Person(string name, string surname, string email) :
            this(name, surname, email, DateTime.Today)
        {
        }

        public Person(string name, string surname, DateTime birthday) :
            this(name, surname, "example@ukma.edu.ua", birthday)
        {
        }

        
        private (string name, string surname, string email, WesternZodiac _westernZodiac, 
            ChineseZodiac _chineseSign, bool _isAdult, bool _isBirthday)
            CreateObject(string name, string surname, string email)
        {
            try
            {
                Task<string> nTask = Task.Run(() => CheckName(name));
                Task<string> sTask = Task.Run(() => CheckSurname(surname));
                Task<string> eTask = Task.Run(() => CheckEmail(email));
                Task<WesternZodiac> wzTask = Task.Run(FindWesternZodiac);
                Task<ChineseZodiac> czTask = Task.Run(FindChineseZodiac);
                Task<int> ageTask = Task.Run(CountAge);
                Task<bool> birthTask = ageTask.ContinueWith((task) => CalculateBirthday(task.Result));
                Task.WaitAll(nTask, sTask, eTask, wzTask, czTask, birthTask);
                return (nTask.Result, sTask.Result, eTask.Result, 
                    wzTask.Result, czTask.Result, (ageTask.Result >= 18), birthTask.Result);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) 
                    throw e.InnerException;
                throw new ApplicationException(@"Undefined exception!");
            }
        }
        
        private string CheckName(string name)
        {
            Thread.Sleep(10);
            Regex regex = new Regex(@"[а-яА-ЯїіІЇєЄA-Za-z]{2,}");
            if(regex.Match(name).Length != name.Length)
                throw new InvalidPersonName(name);
            return name;
        }
        
        private string CheckSurname(string surname)
        {
            Thread.Sleep(20);
            Regex regex = new Regex(@"[а-яА-ЯїіІЇЄєA-Za-z]{2,}");
            if(regex.Match(surname).Length != surname.Length)
                throw new InvalidPersonSurname(surname);
            return surname;
        }

        private string CheckEmail(string email)
        {
            Thread.Sleep(20);
            Regex regex = new Regex(@"^[\w-\.]{2,}@ukma.edu.ua");
            if(regex.Match(email).Length != email.Length)
                throw new NotEduMailException(email);
            return email;
        }
        
        private WesternZodiac FindWesternZodiac()
        {
            Thread.Sleep(20);
            switch (_birthday.Month)
            {
                case 1:
                    return _birthday.Day <= 20 ? WesternZodiac.Capricorn : WesternZodiac.Aquarius;
                case 2:
                    return _birthday.Day <= 19 ? WesternZodiac.Aquarius : WesternZodiac.Pisces;
                case 3:
                    return _birthday.Day <= 20 ? WesternZodiac.Pisces : WesternZodiac.Aries;
                case 4:
                    return _birthday.Day <= 20 ? WesternZodiac.Aries : WesternZodiac.Taurus;
                case 5:
                    return _birthday.Day <= 21 ? WesternZodiac.Taurus : WesternZodiac.Gemini;
                case 6:
                    return _birthday.Day <= 22 ? WesternZodiac.Gemini : WesternZodiac.Cancer;
                case 7:
                    return _birthday.Day <= 22 ? WesternZodiac.Cancer : WesternZodiac.Leo;
                case 8:
                    return _birthday.Day <= 23 ? WesternZodiac.Leo : WesternZodiac.Virgo;
                case 9:
                    return _birthday.Day <= 23 ? WesternZodiac.Virgo : WesternZodiac.Libra;
                case 10:
                    return _birthday.Day <= 23 ? WesternZodiac.Libra : WesternZodiac.Scorpio;
                case 11:
                    return _birthday.Day <= 22 ? WesternZodiac.Scorpio : WesternZodiac.Sagittarius;
                case 12:
                    return _birthday.Day <= 21 ? WesternZodiac.Sagittarius : WesternZodiac.Capricorn;
                default:
                    throw new ArgumentOutOfRangeException($"Undefined WesternZodiac: {_birthday}");
            }
        }

        private ChineseZodiac FindChineseZodiac()
        {
            Thread.Sleep(10);
            try
            {
                ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
                int terrestrialBranch = calendar.GetTerrestrialBranch(calendar.GetSexagenaryYear(_birthday));
                return (ChineseZodiac) terrestrialBranch;
            }
            catch (ArgumentOutOfRangeException)
            {
                return (ChineseZodiac) ((_birthday.Year - 4) % 12 + 1);
            }
        }

        private int CountAge()
        {
            Thread.Sleep(10);
            DateTime now = DateTime.Now;
            int age = now.Year - _birthday.Year;
            if (_birthday > now.AddYears(-age)) 
                age--;
            if (age > 135) 
                throw new OldPersonException(_birthday);
            if (age < 0) 
                throw new NotBornPersonException(_birthday);
            return age;
        }

        private bool CalculateBirthday(int age)
        {
            Thread.Sleep(1000);
            DateTime nextBirthday = _birthday.AddYears(age + 1);
            TimeSpan difference = nextBirthday - DateTime.Today;
            return Convert.ToInt32(difference.TotalDays) == 365;
        }

        public override string ToString()
        {
            return "\nName: " + _name +
                   "\nSurname: " + _surname +
                   "\nE-mail: " + _email +
                   "\nIsAdult: " + _isAdult +
                   "\nIsBirthday: " + _isBirthday +
                   "\nWesternZodiac: " + _westernZodiac +
                   "\nChineseSign: " + _chineseSign;
        }
        
    }
}