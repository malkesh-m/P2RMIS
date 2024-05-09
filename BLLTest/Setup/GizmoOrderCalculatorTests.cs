using NUnit.Framework;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using System;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.Setup
{
    /// <summary>
    /// Unit tests for GizmoOrderCalculator
    /// </summary>
    [TestClass()]
    public class GizmoOrderCalculatorTests
    {
        #region Overhead
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion
        #region Test data
        private static MechanismTemplateElement Criteria1 = new MechanismTemplateElement { MechanismTemplateElementId = 1};
        private static MechanismTemplateElement Criteria2 = new MechanismTemplateElement { MechanismTemplateElementId = 2};
        private static MechanismTemplateElement Criteria3 = new MechanismTemplateElement { MechanismTemplateElementId = 3};
        private static MechanismTemplateElement Criteria4 = new MechanismTemplateElement { MechanismTemplateElementId = 4};
        private static MechanismTemplateElement Criteria5 = new MechanismTemplateElement { MechanismTemplateElementId = 5};
        private static MechanismTemplateElement Criteria6 = new MechanismTemplateElement { MechanismTemplateElementId = 6};
        private static MechanismTemplateElement Criteria7 = new MechanismTemplateElement { MechanismTemplateElementId = 7};
        private static MechanismTemplateElement Criteria8 = new MechanismTemplateElement { MechanismTemplateElementId = 8};
        #endregion
        #region Tests
        /// <summary>
        /// Test success helpers to set up data
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestSetup()
        {
            //
            // Set up local data
            //
            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(2, 4, 9, Order.Up);

            Assert.AreEqual(8, c.Count(), "Add or constructor did not work");
            Assert.AreEqual(1, Criteria1.SortOrder, "sort order 1 not correct");
            Assert.AreEqual(2, Criteria2.SortOrder, "sort order 2 not correct");
            Assert.AreEqual(3, Criteria3.SortOrder, "sort order 3 not correct");
            Assert.AreEqual(4, Criteria4.SortOrder, "sort order 4 not correct");
            Assert.AreEqual(5, Criteria5.SortOrder, "sort order 5 not correct");
            Assert.AreEqual(6, Criteria6.SortOrder, "sort order 6 not correct");
            Assert.AreEqual(7, Criteria7.SortOrder, "sort order 7 not correct");
            Assert.AreEqual(8, Criteria8.SortOrder, "sort order 8 not correct");
        }
        /// <summary>
        /// Test success helpers to set up data
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestCalculatorApply()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 3;
            int newValueOrder = 6;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Up);
            c.ReOrder();
            c.Apply();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();
            for (int i = 0; i < 7; i++) { Console.WriteLine("#" +i + "   CurrentValue  " + l[i].CurrentValue + " - " + "ComputedValue for " + " - " + l[i].ComputedValue + " SortOrder " + l[i].Entity.SortOrder ); }
            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(10, l[0].Entity.ModifiedBy, "updates not set for entry 1");
            Assert.AreEqual(10, l[1].Entity.ModifiedBy, "updates not set for entry 2");
            Assert.AreEqual(10, l[2].Entity.ModifiedBy, "updates not set for entry 4");
            Assert.AreEqual(10, l[3].Entity.ModifiedBy, "updates not set for entry 5");
            Assert.AreEqual(10, l[4].Entity.ModifiedBy, "updates not set for entry 6");
            Assert.AreEqual(10, l[5].Entity.ModifiedBy, "updates not set for entry 7");
            Assert.AreEqual(10, l[6].Entity.ModifiedBy, "updates not set for entry 8");

            Assert.AreEqual(1, l[0].Entity.SortOrder, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].Entity.SortOrder, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].Entity.SortOrder, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].Entity.SortOrder, "value not calculated for entry 5");
            Assert.AreEqual(5, l[4].Entity.SortOrder, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].Entity.SortOrder, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].Entity.SortOrder, "value not calculated for entry 8");


        }
        #region Up Tests
        /// <summary>
        /// Test up calculation - middle #3 -> #6
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Up_Middle1()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 3;
            int newValueOrder = 6;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Up);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();
            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }
            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #3 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Up_Middle2()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 3;
            int newValueOrder = 8;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Up);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(6, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #1 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Up_Middle3()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 1;
            int newValueOrder = 8;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Up);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(6, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #1 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Up_Middle4()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 4;
            int newValueOrder = 5;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Up);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation with a null
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Up_Null()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 4;
            int newValueOrder = 5;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = SummaryPopulate(oldValueOrder, newValueOrder, skip, Order.Up);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");
            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.IsNull(l[5].ComputedValue, "value calculated for entry 7");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 8");
        }
        #endregion
        #region Test Down
        /// <summary>
        /// Test up calculation - middle #3 -> #6
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Down_Middle1()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 6;
            int newValueOrder = 3;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Down);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(4, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #3 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Down_Middle2()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 8;
            int newValueOrder = 3;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Down);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(4, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #1 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Down_Middle3()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 8;
            int newValueOrder = 1;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Down);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(2, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(3, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(4, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #1 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Down_Middle4()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 5;
            int newValueOrder = 4;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Down);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[5].ComputedValue, "value not calculated for entry 7");
            Assert.AreEqual(8, l[6].ComputedValue, "value not calculated for entry 8");
        }
        /// <summary>
        /// Test up calculation - middle #1 -> #8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_Down_Null()
        {
            //
            // Set up local data
            //
            int oldValueOrder = 4;
            int newValueOrder = 5;
            int skip = oldValueOrder;

            GizmoOrderCalculator<MechanismTemplateElement> c = SummaryPopulate(oldValueOrder, newValueOrder, skip, Order.Down);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");
            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[4].ComputedValue, "value not calculated for entry 6");
            Assert.IsNull(l[5].ComputedValue, "value calculated for entry 7");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 8");

        }
        #endregion
        #region Test Insert
        /// <summary>
        /// Test insert calculation - middle insert @ 3 out of 4
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_InsertMiddle()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = null;
            int newValueOrder = 3;
            int skip = 99;

            GizmoOrderCalculator<MechanismTemplateElement> c = ShortPopulate(oldValueOrder, newValueOrder, skip, Order.Insert);
            Assert.AreEqual(4, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 4; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(4, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 4");
        }
        /// <summary>
        /// Test insert calculation - middle insert @ 1 out of 4
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_InsertBeginning()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = null;
            int newValueOrder = 1;
            int skip = 99;

            GizmoOrderCalculator<MechanismTemplateElement> c = ShortPopulate(oldValueOrder, newValueOrder, skip, Order.Insert);
            Assert.AreEqual(4, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 4; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(2, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(3, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(4, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(5, l[3].ComputedValue, "value not calculated for entry 4");
        }
        /// <summary>
        /// Test insert calculation - middle insert @ 5 out of 4
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_InsertEnd()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = null;
            int newValueOrder = 5;
            int skip = 99;

            GizmoOrderCalculator<MechanismTemplateElement> c = ShortPopulate(oldValueOrder, newValueOrder, skip, Order.Insert);
            Assert.AreEqual(4, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 4; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            foreach (var a in l) { Assert.NotNull(a.ComputedValue, "something not calculated"); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 4");
        }
        #endregion
        #region Test Remove
        /// <summary>
        /// Test remove calculation - remove #3 out of 8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_RemoveMiddle()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = 4;
            int? newValueOrder = null;
            int skip = oldValueOrder.Value;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Remove);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[5].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 7");
        }
        /// <summary>
        /// Test remove calculation - remove #8 out of 8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_RemoveEnd()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = 8;
            int? newValueOrder = null;
            int skip = oldValueOrder.Value;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Remove);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[5].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 7");
        }
        /// <summary>
        /// Test remove calculation - remove #1 out of 8
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void Test_RemoveStart()
        {
            //
            // Set up local data
            //
            int? oldValueOrder = 1;
            int? newValueOrder = null;
            int skip = oldValueOrder.Value;

            GizmoOrderCalculator<MechanismTemplateElement> c = Populate(oldValueOrder, newValueOrder, skip, Order.Remove);
            Assert.AreEqual(7, c.Count(), "Add or constructor did not work");

            c.ReOrder();

            List<Gizmo<MechanismTemplateElement>> l = c.Result().ToList();

            for (int i = 0; i < 7; i++) { Console.WriteLine("value for " + i + " " + l[i].ComputedValue); }

            Assert.AreEqual(1, l[0].ComputedValue, "value not calculated for entry 1");
            Assert.AreEqual(2, l[1].ComputedValue, "value not calculated for entry 2");
            Assert.AreEqual(3, l[2].ComputedValue, "value not calculated for entry 3");
            Assert.AreEqual(4, l[3].ComputedValue, "value not calculated for entry 4");
            Assert.AreEqual(5, l[4].ComputedValue, "value not calculated for entry 5");
            Assert.AreEqual(6, l[5].ComputedValue, "value not calculated for entry 6");
            Assert.AreEqual(7, l[6].ComputedValue, "value not calculated for entry 7");
        }
        #endregion
        #region Helper tests
        /// <summary>
        /// Test helper for up direction
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperDirectionUp()
        {
            Order result = GizmoHelper.DirectionIs(3, 5);
            Assert.AreEqual(Order.Up, result, "Direction was not up");
        }
        /// <summary>
        /// Test helper for down direction
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperDirectionDown()
        {
            Order result = GizmoHelper.DirectionIs(4, 2);
            Assert.AreEqual(Order.Down, result, "Direction was not down");
        }
        /// <summary>
        /// Test helper for Insert direction
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperDirectionInsert()
        {
            Order result = GizmoHelper.DirectionIs(null, 2);
            Assert.AreEqual(Order.Insert, result, "Direction was not insert");
        }
        /// <summary>
        /// Test helper for Insert direction
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperDirectionEqual()
        {
            Order result = GizmoHelper.DirectionIs(2, 2);
            Assert.AreEqual(Order.Equal, result, "Direction was not equal");
        }
        /// <summary>
        /// Test helper for helper calculator creation
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperSortOrderCalculator()
        {
            int oldValue = 1;
            int newValue = 2;
            Order direction = Order.Down;
            MechanismTemplate template = SortOrderSetup();
            int id = 4;
            int userId = 10;
            
            SortOrderSetup();
            var c = GizmoHelper.CreateSortOrderCalculator(oldValue, newValue, direction, template, id, userId);

            Assert.AreEqual(template.MechanismTemplateElements.Count() - 1, c.Count(), "template does not contain correct # elements");
            Assert.IsFalse(c.Result().Any(x => x.Entity.MechanismTemplateElementId == id), "list contained an element that it should not have");
            Assert.AreEqual(oldValue, c.OldValue);
            Assert.AreEqual(newValue, c.NewValue);
            Assert.AreEqual(direction, c.Direction);
            Assert.AreEqual(userId, c.UserId);

            int computed = 99;
            var a = c.Result().ElementAt(0);
            a.ComputedValue = computed;
            a.Apply();
            Assert.AreEqual(computed, a.Entity.SortOrder, "set property not correct");
        }
        /// <summary>
        /// Test helper for helper calculator creation
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestHelperSummarySortOrderCalculator()
        {
            int oldValue = 1;
            int? newValue = null;
            Order direction = Order.Down;
            MechanismTemplate template = SortOrderSetup();
            int id = 4;
            int userId = 10;

            SortOrderSetup();
            var c = GizmoHelper.CreateSummarySortOrderCalculator(oldValue, newValue, direction, template, id, userId);

            Assert.AreEqual(template.MechanismTemplateElements.Count() - 1, c.Count(), "template does not contain correct # elements");
            Assert.IsFalse(c.Result().Any(x => x.Entity.MechanismTemplateElementId == id), "list contained an element that it should not have");
            Assert.AreEqual(oldValue, c.OldValue);
            Assert.AreEqual(newValue, c.NewValue);
            Assert.AreEqual(direction, c.Direction);
            Assert.AreEqual(userId, c.UserId);

            int computed = 99;
            var a = c.Result().ElementAt(0);
            a.ComputedValue = computed;
            a.Apply();
            Assert.AreEqual(computed, a.Entity.SummarySortOrder, "set property not correct");
        }
        #endregion
        #region Gizmo Tests
        /// <summary>
        /// Test Gizmo Apply method
        /// </summary>
        [TestMethod()]
        [Category("GizmoOrderCalculatorTests")]
        public void TestGizmoApply()
        {
            int Computed = 9;
            var g = new Gizmo<MechanismTemplateElement>(Criteria2, x => x.SortOrder, s => { Criteria2.SortOrder = s.Value; });
            g.ComputedValue = Computed;
            g.Apply();

            Assert.AreEqual(Computed, Criteria2.SortOrder, "Gizmo.Apply did not work!");
        }
        #endregion
        #endregion
        #region Helpers
        private MechanismTemplate SortOrderSetup()
        {
            InitializeData();

            MechanismTemplate template = new MechanismTemplate();
            template.MechanismTemplateElements.Add(Criteria1);
            template.MechanismTemplateElements.Add(Criteria2);
            template.MechanismTemplateElements.Add(Criteria3);
            template.MechanismTemplateElements.Add(Criteria4);
            template.MechanismTemplateElements.Add(Criteria5);
            template.MechanismTemplateElements.Add(Criteria6);
            template.MechanismTemplateElements.Add(Criteria7);
            template.MechanismTemplateElements.Add(Criteria8);

            return template;
        }
        private GizmoOrderCalculator<MechanismTemplateElement> SummaryPopulate(int oldValue, int newValue, int notThisOne, Order direction)
        {
            InitializeData();
            GizmoOrderCalculator<MechanismTemplateElement> c = new GizmoOrderCalculator<MechanismTemplateElement>(oldValue, newValue, direction, 10);
            Gizmo<MechanismTemplateElement> g = new Gizmo<MechanismTemplateElement>(Criteria1, x => x.SortOrder, s => { Criteria1.SortOrder = s.Value; });
            if (notThisOne != 1) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria2, x => x.SummarySortOrder, s => { Criteria2.SortOrder = s.Value; });
            if (notThisOne != 2) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria3, x => x.SummarySortOrder, s => { Criteria3.SortOrder = s.Value; });
            if (notThisOne != 3) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria4, x => x.SummarySortOrder, s => { Criteria4.SortOrder = s.Value; });
            if (notThisOne != 4) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria5, x => x.SummarySortOrder, s => { Criteria5.SortOrder = s.Value; });
            if (notThisOne != 5) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria6, x => x.SummarySortOrder, s => { Criteria6.SortOrder = s.Value; });
            if (notThisOne != 6) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria7, x => x.SummarySortOrder, s => { Criteria7.SortOrder = s.Value; });
            if (notThisOne != 7) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria8, x => x.SummarySortOrder, s => { Criteria8.SortOrder = s.Value; });
            if (notThisOne != 8) { c.Add(g); }

            return c;
        }

        private GizmoOrderCalculator<MechanismTemplateElement> Populate(int? oldValue, int? newValue, int notThisOne, Order direction)
        {
            InitializeData();
            GizmoOrderCalculator<MechanismTemplateElement> c = new GizmoOrderCalculator<MechanismTemplateElement>(oldValue, newValue, direction, 10);
            Gizmo<MechanismTemplateElement> g = new Gizmo<MechanismTemplateElement>(Criteria1, x => x.SortOrder, s => { Criteria1.SortOrder = s.Value; });
            if (notThisOne != 1) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria2, x => x.SortOrder, s => { Criteria2.SortOrder = s.Value; });
            if (notThisOne != 2) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria3, x => x.SortOrder, s => { Criteria3.SortOrder = s.Value; });
            if (notThisOne != 3) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria4, x => x.SortOrder, s => { Criteria4.SortOrder = s.Value; });
            if (notThisOne != 4) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria5, x => x.SortOrder, s => { Criteria5.SortOrder = s.Value; });
            if (notThisOne != 5) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria6, x => x.SortOrder, s => { Criteria6.SortOrder = s.Value; });
            if (notThisOne != 6) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria7, x => x.SortOrder, s => { Criteria7.SortOrder = s.Value; });
            if (notThisOne != 7) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria8, x => x.SortOrder, s => { Criteria8.SortOrder = s.Value; });
            if (notThisOne != 8) { c.Add(g); }

            return c;
        }

        private GizmoOrderCalculator<MechanismTemplateElement> ShortPopulate(int? oldValue, int newValue, int notThisOne, Order direction)
        {
            InitializeData();
            GizmoOrderCalculator<MechanismTemplateElement> c = new GizmoOrderCalculator<MechanismTemplateElement>(oldValue, newValue, direction, 10);
            Gizmo<MechanismTemplateElement> g = new Gizmo<MechanismTemplateElement>(Criteria1, x => x.SortOrder, s => { Criteria1.SortOrder = s.Value; });
            if (notThisOne != 1) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria2, x => x.SortOrder, s => { Criteria2.SortOrder = s.Value; });
            if (notThisOne != 2) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria3, x => x.SortOrder, s => { Criteria3.SortOrder = s.Value; });
            if (notThisOne != 3) { c.Add(g); }

            g = new Gizmo<MechanismTemplateElement>(Criteria4, x => x.SortOrder, s => { Criteria4.SortOrder = s.Value; });
            if (notThisOne != 4) { c.Add(g); }

            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeData()
        {
            Criteria1.SortOrder = 1;
            Criteria2.SortOrder = 2;
            Criteria3.SortOrder = 3;
            Criteria4.SortOrder = 4;
            Criteria5.SortOrder = 5;
            Criteria6.SortOrder = 6;
            Criteria7.SortOrder = 7;
            Criteria8.SortOrder = 8;

            Criteria1.SummarySortOrder = 1;
            Criteria2.SummarySortOrder = 2;
            Criteria3.SummarySortOrder = 3;
            Criteria4.SummarySortOrder = 4;
            Criteria5.SummarySortOrder = 5;
            Criteria6.SummarySortOrder = 6;
            Criteria7.SummarySortOrder = null;
            Criteria8.SummarySortOrder = 7;

            Criteria1.ModifiedDate = null;
            Criteria2.ModifiedDate = null;
            Criteria3.ModifiedDate = null;
            Criteria4.ModifiedDate = null;
            Criteria5.ModifiedDate = null;
            Criteria6.ModifiedDate = null;
            Criteria7.ModifiedDate = null;
            Criteria8.ModifiedDate = null;

            Criteria1.ModifiedBy = null;
            Criteria2.ModifiedBy = null;
            Criteria3.ModifiedBy = null;
            Criteria4.ModifiedBy = null;
            Criteria5.ModifiedBy = null;
            Criteria6.ModifiedBy = null;
            Criteria7.ModifiedBy = null;
            Criteria8.ModifiedBy = null;
        }
        #endregion
    }
}
