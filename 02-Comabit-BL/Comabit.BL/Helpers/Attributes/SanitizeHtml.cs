// <copyright file="SanitizeHtml.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SanitizeHtml : Attribute
    {
    }
}