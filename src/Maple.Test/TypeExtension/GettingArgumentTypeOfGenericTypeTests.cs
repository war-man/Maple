﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Maple.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Maple.Test
{
    [TestClass]
    public sealed class GettingArgumentTypeOfGenericTypeTests
    {
        [TestMethod]
        public void When_checking_type_of_null()
        {
            var e = Should.Throw<ArgumentNullException>(() =>
            {
                ((Type)null).TryGetGenericArguments(out var result);
            });

            e.ParamName.ShouldBe("type");
        }

        [TestMethod]
        public void When_checking_type_of_non_generic_class()
        {
            typeof(NonGenericType)
                .TryGetGenericArguments(out var result)
                .ShouldBeFalse();

            result.ShouldBeNull();
        }

        [TestMethod]
        public void When_checking_type_of_generic_class_with_single_argument()
        {
            typeof(SingleGenericType<string>)
                .TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.ShouldNotBeNull();
            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(string));
        }

        [TestMethod]
        public void When_checking_type_of_generic_class_with_double_arguments()
        {
            typeof(DoubleGenericType<int, string>)
                .TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.ShouldNotBeNull();
            result.Length.ShouldBe(2);
            result[0].ShouldBe(typeof(int));
            result[1].ShouldBe(typeof(string));
        }

        [TestMethod]
        public void When_checking_type_of_generic_class_with_multiple_arguments()
        {
            typeof(MultipleGenericType<int, string, DateTime, string, short>)
                .TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.ShouldNotBeNull();
            result.Length.ShouldBe(5);
            result[0].ShouldBe(typeof(int));
            result[1].ShouldBe(typeof(string));
            result[2].ShouldBe(typeof(DateTime));
            result[3].ShouldBe(typeof(string));
            result[4].ShouldBe(typeof(short));
        }

        [TestMethod]
        public void When_checking_type_of_generic_array()
        {
            typeof(int[]).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(int));
        }

        [TestMethod]
        public void When_checking_type_of_generic_list()
        {
            typeof(List<byte>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(byte));
        }

        [TestMethod]
        public void When_checking_type_of_generic_queue()
        {
            typeof(Queue<DateTime>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(DateTime));
        }

        [TestMethod]
        public void When_checking_type_of_generic_stack()
        {
            typeof(Stack<DateTime>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(DateTime));
        }

        [TestMethod]
        public void When_checking_type_of_generic_collection()
        {
            typeof(Collection<DateTime>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(DateTime));
        }

        [TestMethod]
        public void When_checking_type_of_generic_hash_set()
        {
            typeof(HashSet<DateTime>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(DateTime));
        }

        [TestMethod]
        public void When_checking_type_of_generic_linked_list()
        {
            typeof(LinkedList<DateTime>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(DateTime));
        }

        [TestMethod]
        public void When_checking_type_of_generic_dictionary()
        {
            typeof(Dictionary<DateTime, TimeSpan>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(2);
            result[0].ShouldBe(typeof(DateTime));
            result[1].ShouldBe(typeof(TimeSpan));
        }

        [TestMethod]
        public void When_checking_type_of_generic_collection_of_key_value()
        {
            typeof(ICollection<KeyValuePair<DateTime, TimeSpan>>).TryGetGenericArguments(out var result)
                .ShouldBeTrue();

            result.Length.ShouldBe(1);
            result[0].ShouldBe(typeof(KeyValuePair<DateTime, TimeSpan>));
        }

        private class NonGenericType { }

        private class SingleGenericType<T> { }

        private class DoubleGenericType<T1, T2> { }

        private class MultipleGenericType<T1, T2, T3, T4, T5> { }
    }
}