using NUnit.Framework;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace BLLTest.Rules
{
    /// <summary>
    /// Unit tests for RuleEngine
    /// </summary>
    [TestClass()]
    public class RuleEngineTests
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
        #region Tests

        /// <summary>
        /// Test success from rule
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void Test1Rule()
        {
            //
            // Set up local data
            //
            List<IRuleBase> a = new List<IRuleBase>();

            R1 r1 = new R1(null, null, new List<CrudAction> { CrudAction.Add });

            a.Add(r1);

            var e = new RuleEngine<SessionPanel>(a, CrudAction.Add, null);
            //
            // Test
            //
            e.Apply();

            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
        }
        /// <summary>
        /// Test success from rule with 2 
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void Test2Rule()
        {
            //
            // Set up local data
            //
            List<IRuleBase> a = new List<IRuleBase>();

            R1 r1 = new R1(null, null, new List<CrudAction> { CrudAction.Add });
            R2 r2 = new R2(null, null, new List<CrudAction> { CrudAction.Add });

            a.Add(r1);
            a.Add(r2);

            var e = new RuleEngine<SessionPanel>(a, CrudAction.Add, null);
            //
            // Test
            //
            e.Apply();

            //
            // Verify
            //
            Assert.AreEqual(1, e.Messages.Count());
            Assert.AreEqual(R2.EMessage, e.Messages[0]);
        }
        /// <summary>
        /// Test success from rule with 2 
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void Test2RulesDifferentActions1()
        {
            //
            // Set up local data
            //
            List<IRuleBase> a = new List<IRuleBase>();

            R1 r1 = new R1(null, null, new List<CrudAction> { CrudAction.Add });
            R2 r2 = new R2(null, null, new List<CrudAction> { CrudAction.Modify });

            a.Add(r1);
            a.Add(r2);

            var e = new RuleEngine<SessionPanel>(a, CrudAction.Add, null);
            //
            // Test
            //
            e.Apply();

            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
        }
        /// <summary>
        /// Test success from rule with 2 
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void Test2RulesDifferentActions0()
        {
            //
            // Set up local data
            //
            List<IRuleBase> a = new List<IRuleBase>();

            R1 r1 = new R1(null, null, new List<CrudAction> { CrudAction.Delete });
            R2 r2 = new R2(null, null, new List<CrudAction> { CrudAction.Modify });

            a.Add(r1);
            a.Add(r2);

            var e = new RuleEngine<SessionPanel>(a, CrudAction.Add, null);
            //
            // Test
            //
            e.Apply();

            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
        }
        /// <summary>
        /// Test default constructor 
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void TestDefaultConstructor()
        {
            //
            // Set up local data
            //
            var e = new RuleEngine<SessionPanel>();
            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
            Assert.AreEqual(CrudAction.Default, e.Action);
            Assert.AreEqual(0, e.RulesApplied);
        }
        /// <summary>
        /// Test default constructor 
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void TestRunDefaultConstructor()
        {
            //
            // Set up local data
            //
            var e = new RuleEngine<SessionPanel>();
            //
            // Test
            //
            e.Apply();
            e.Apply();
            e.Apply();
            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
            Assert.AreEqual(CrudAction.Default, e.Action);
            Assert.AreEqual(0, e.RulesApplied);
        }
        
        /// <summary>
        /// Test parameter block gets to action
        /// </summary>
        [TestMethod()]
        [Category("RuleEngine")]
        public void TestBlockGetsToAction()
        {
            //
            // Set up local data
            //
            List<IRuleBase> a = new List<IRuleBase>();

            MyBlock block = new MyBlock();
            block.MyParameter = 456;

            R3 r3 = new R3(null, null, block, new List<CrudAction> { CrudAction.Add });

            var e = new RuleEngine<SessionPanel>(a, CrudAction.Add, block);
            //
            // Test
            //
            e.Apply();

            //
            // Verify
            //
            Assert.AreEqual(0, e.Messages.Count());
        }
        #endregion
    }
    internal class R1 : RuleBase<SessionPanel>
    {
        public R1(IUnitOfWork unitOfWork, SessionPanel entity, IList<CrudAction> action) : base(unitOfWork, entity, action)
        {

        }
        public override void Apply(ICrudBlock block)
        {
            this.IsBroken = false;
        }
    }
    
    internal class R2 : RuleBase<SessionPanel>
    {
        public const string EMessage = "I broke this in R2";
        public R2(IUnitOfWork unitOfWork, SessionPanel entity, IList<CrudAction> action) : base(unitOfWork, entity, action)
        {

        }
        public override void Apply(ICrudBlock block)
        {
            this.IsBroken = true;
            this.Message = R2.EMessage;
        }
    }

    internal class R3 : RuleBase<SessionPanel>
    {
        public const string EMessage = "No block in Apply()";
        public R3(IUnitOfWork unitOfWork, SessionPanel entity, CrudBlock<SessionPanel> block, IList<CrudAction> action) : base(unitOfWork, entity, action)
        {

        }
        public override void Apply(ICrudBlock block)
        {
            this.IsBroken = (block == null);
            if (!IsBroken)
            {
                //
                // Because the parameter is only marked in a generic
                // way we need to cast it to the specific type to access
                // a specific value.
                //
                MyBlock a = block as MyBlock;
                this.IsBroken = (a.MyParameter != 456);
            }
            this.Message = (IsBroken) ? R3.EMessage : string.Empty;
        }
    }

    internal class MyBlock : CrudBlock<SessionPanel>
    {
        public int MyParameter { get; set; }
    }
}
