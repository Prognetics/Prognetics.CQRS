﻿namespace Prognetics.CQRS.Tests.Shared.GenericCommand
{
    public class SomeEntity
    {
        public SomeEntity(string data)
        {
            Data = data;
        }

        public string Data { get; }
    }
}