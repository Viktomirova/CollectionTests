using NUnit.Framework;
using System.Linq;
using System;
using Collections;

namespace CollectionsTests
{
    public class CollectionTests
    {
        Collection<int> nums;
        Collection<string> names;

        [SetUp]
        public void SetUp()
        {
            nums = new Collection<int>(10, 20, 30);
            names = new Collection<string>("George", "Pesho");
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count, Is.EqualTo(itemsCount));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        
        [Test]
        public void Test_Collection_Add()
        {
            nums.Add(40);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            names.AddRange("Kolyo", "Misho", "Tony");
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho, Kolyo, Misho, Tony]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            nums = new Collection<int>(10, 20, 30);
            int oldCapacity = nums.Capacity;
            nums.AddRange(Enumerable.Range(40, 50).ToArray());
            string expectedNums = $"[10, 20, 30, {string.Join(", ", Enumerable.Range(40, 50))}]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
                
        [Test]
        public void Test_Collection_AddWithGrow()
        {
            int oldCapacity = nums.Capacity;

            for (int i = 1; i <= 20; i++)
            {
                nums.Add(i);
            }
            string expectedNums = string.Join(", ", nums);

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        }
                
        [Test]
        public void Test_Collection_Clear()
        {
            names.Clear();
            Assert.That(names.Count, Is.EqualTo(0));
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            nums = new Collection<int>(5);
            Assert.That(nums.Count, Is.EqualTo(1));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            var nums = new Collection<int>();
            const int itemsCount = 10;
            for (int i = 1; i <= itemsCount; i++)
            {
                nums.Add(i);
                Assert.That(nums.Count, Is.EqualTo(i));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
            for (int i = itemsCount; i >= 1; i--)
            {
                nums.RemoveAt(i - 1);
                Assert.That(nums.Count, Is.EqualTo(i - 1));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            nums = new Collection<int>();
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.AreEqual(0, nums.Count);
            Assert.That(16, Is.EqualTo(nums.Capacity));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            names.AddRange("Kolyo", "Tosho");
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho, Kolyo, Tosho]"));
            names.Exchange(0, 3);
            Assert.That(names.ToString(), Is.EqualTo("[Tosho, Pesho, Kolyo, George]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            Assert.That(() => names.Exchange(-1, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(1, -1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(2, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.Exchange(1, 2), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
        }
                
        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            names.AddRange("Kolyo", "Tosho");
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho, Kolyo, Tosho]"));
            names.Exchange(1, 2);
            Assert.That(names.ToString(), Is.EqualTo("[George, Kolyo, Pesho, Tosho]"));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            string name1 = names[0];
            Assert.That(name1, Is.EqualTo("George"));
            string name2 = names[1];
            Assert.That(name2, Is.EqualTo("Pesho"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            Assert.That(() => { string name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { string name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
        }
        
        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            names.InsertAt(2, "Kolyo");
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho, Kolyo]"));
        }
        
        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            Assert.That(() => names.InsertAt(-1, "Misho"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(3, "Kolyo"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            names.InsertAt(1, "Kolyo");
            Assert.That(names.ToString(), Is.EqualTo("[George, Kolyo, Pesho]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            names.InsertAt(0, "Kolyo");
            Assert.That(names.ToString(), Is.EqualTo("[Kolyo, George, Pesho]"));
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            int oldCapacity = names.Capacity;
            names.InsertAt(0, "Kolyo");
            names.InsertAt(3, "Misho");
            for (int i = names.Count; i >= 0; i--)
            {
                names.InsertAt(i, "Name" + i);
            }
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(names.ToString(), Is.EqualTo("[Name0, Kolyo, Name1, George, Name2, Pesho, Name3, Misho, Name4]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }
        
        [Test]
        public void Test_Collection_RemoveAll()
        {
            var nums = new Collection<int>();
            const int itemsCount = 10;
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            for (int i = 1; i <= itemsCount; i++)
            {
                var removed = nums.RemoveAt(0);
                Assert.That(removed, Is.EqualTo(i));
            }
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var removed = names.RemoveAt(1);
            Assert.That(removed, Is.EqualTo("Pesho"));
            Assert.That(names.ToString(), Is.EqualTo("[George]"));
        }
        
        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            Assert.That(() => names.RemoveAt(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(2), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
        }
        
        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            names.Add("Kolyo");
            var removed = names.RemoveAt(1);
            Assert.That(removed, Is.EqualTo("Pesho"));
            Assert.That(names.ToString(), Is.EqualTo("[George, Kolyo]"));
        }
        
        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var removed = names.RemoveAt(0);
            Assert.That(removed, Is.EqualTo("George"));
            Assert.That(names.ToString(), Is.EqualTo("[Pesho]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
            names[0] = "Kolyo";
            names[1] = "Misho";
            Assert.That(names.ToString(), Is.EqualTo("[Kolyo, Misho]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            Assert.That(() => { names[-1] = "new name"; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[-2] = "new name"; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Pesho]"));
        }
        
        [Test]
        public void Test_Collection_ToStringCollectionOfCollections()
        {
            var names = new Collection<string>("Kolyo", "Pesho");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var allTypes = new Collection<object>(names, nums, dates);
            Assert.That(allTypes.ToString(), Is.EqualTo("[[Kolyo, Pesho], [10, 20], []]"));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            var names = new Collection<string>();
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            var objects = new Collection<object>("Kolyo", "Pesho", 20);
            Assert.That(objects.ToString(), Is.EqualTo("[Kolyo, Pesho, 20]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            var names = new Collection<string>("George");
            Assert.That(names.ToString(), Is.EqualTo("[George]"));
        }
    }
}