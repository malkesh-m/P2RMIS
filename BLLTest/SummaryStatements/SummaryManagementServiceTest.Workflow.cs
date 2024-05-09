using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit test for SummaryManagementService workflow management:
    ///  - GetWorkflowAssignmentOrDefault()
    ///  - GetClientWorkflowAll()
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest
    {
        #region GetWorkflowAssignmentOrDefault Tests
        /// <summary>
        /// Test with a good program and fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        public void GetWorkflowAssignmentOrDefault_Test()
        {
            //
            // Set  up the data to return
            //
            List<IAwardWorkflowPriorityModel> list = new List<IAwardWorkflowPriorityModel>();
            AwardWorkflowPriorityModel model = new AwardWorkflowPriorityModel();
            list.Add(model);

            GetWorkflowAssignmentOrDefaultSuccessTest(list, 202, 2022, 1);
        }
        /// <summary>
        /// Test - Blank program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramBlank_FiscalYear_EmptyTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 0, null);
        }
        /// <summary>
        /// Test - Blank program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramBlank_FiscalYear_WhiteSpaceTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 0, null);
        }
        /// <summary>
        /// Test - Blank program & valid value for fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramBlankTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 2022, null);
        }

        /// <summary>
        /// Test -  white space  program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramWhiteSpace_FiscalYear_EmptyTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 0, null);
        }
        /// <summary>
        /// Test -  white space  program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramWhiteSpace_FiscalYear_WhiteSpaceTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 0, null);
        }
        /// <summary>
        /// Test - white space program & valid value for fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_ProgramWhiteSpaceTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(0, 2022, 1);
        }
        /// <summary>
        /// Test - program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_Program_FiscalYear_EmptyTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(202, 0, 1);
        }
        /// <summary>
        /// Test - program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetWorkflowAssignmentOrDefault")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetWorkflowAssignmentOrDefault_Program_FiscalYear_WhiteSpaceTest()
        {
            GetWorkflowAssignmentOrDefaultFailTest(202, 0, 1);
        }
        #region Helpers
        /// <summary>
        /// Failure tests for GetWorkflowAssignmentOrDefault will execute the 
        /// mocks & steps in this method.
        /// </summary>
        private void GetWorkflowAssignmentOrDefaultFailTest(int program, int fiscalYear, int? cycle)
        {
            var container = this.testService.GetWorkflowAssignmentOrDefault(program, fiscalYear, cycle);
        }
        /// <summary>
        /// Successful tests for GetClientWorkflowAll will execute the 
        /// mocks & steps in this method.
        /// </summary>
        private void GetWorkflowAssignmentOrDefaultSuccessTest(List<IAwardWorkflowPriorityModel> theList, int program, int fiscalYear, int? cycle)
        {
            //
            // Set up expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetWorkflowAssignmentOrDefault(program, fiscalYear, cycle)).Return(theList);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            var container = testService.GetWorkflowAssignmentOrDefault(program, fiscalYear, cycle);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<IAwardWorkflowPriorityModel>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }

        #endregion
        #endregion
        #region GetClientWorkflowAll Tests
        /// <summary>
        /// Test with a good program and fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        public void GetClientWorkflowAll_Test()
        {
            //
            // Set  up the data to return
            //
            List<IWorkflowTemplateModel> list = new List<IWorkflowTemplateModel>();
            WorkflowTemplateModel model = new WorkflowTemplateModel();
            list.Add(model);

            GetClientWorkflowAllSuccessTest(list, 202, 2022);
        }


        /// <summary>
        /// Test - Blank program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramBlank_FiscalYear_EmptyTest()
        {
            GetClientWorkflowAllFailTest(0, 0);
        }
        /// <summary>
        /// Test - Blank program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramBlank_FiscalYear_WhiteSpaceTest()
        {
            GetClientWorkflowAllFailTest(0, 0);
        }
        /// <summary>
        /// Test - Blank program & valid value for fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramBlankTest()
        {
            GetClientWorkflowAllFailTest(0, 2022);
        }
        /// <summary>
        /// Test -  white space  program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramWhiteSpace_FiscalYear_EmptyTest()
        {
            GetClientWorkflowAllFailTest(0, 0);
        }
        /// <summary>
        /// Test -  white space  program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramWhiteSpace_FiscalYear_WhiteSpaceTest()
        {
            GetClientWorkflowAllFailTest(0, 0);
        }
        /// <summary>
        /// Test - white space program & valid value for fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_ProgramWhiteSpaceTest()
        {
            GetClientWorkflowAllFailTest(0, 2022);
        }
        /// <summary>
        /// Test - program & blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_Program_FiscalYear_EmptyTest()
        {
            GetClientWorkflowAllFailTest(202, 0);
        }
        /// <summary>
        /// Test - program & white space fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetClientWorkflowAll")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetClientWorkflowAll_Program_FiscalYear_WhiteSpaceTest()
        {
            GetClientWorkflowAllFailTest(202, 0);
        }
        #region Helpers
        /// <summary>
        /// Failure tests for GetClientWorkflowAll will execute the 
        /// mocks & steps in this method.
        /// </summary>
        private void GetClientWorkflowAllFailTest(int program, int fiscalYear)
        {
            var container = this.testService.GetClientWorkflowAll(program, fiscalYear);
        }
        /// <summary>
        /// Successful tests for GetClientWorkflowAll will execute the 
        /// mocks & steps in this method.
        /// </summary>
        private void GetClientWorkflowAllSuccessTest(List<IWorkflowTemplateModel> theList, int program, int fiscalYear)
        {
            //
            // Set up expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetClientWorkflowAll(program, fiscalYear)).Return(theList);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            var container = testService.GetClientWorkflowAll(program, fiscalYear);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<IWorkflowTemplateModel>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        #endregion
        #endregion
    }
}
