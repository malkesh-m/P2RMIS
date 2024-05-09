using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository;

namespace DLLTest
{
    /// <summary>
    /// Base for unit tests.  Collection of "stuff" to simplify unit tests.
    /// </summary>
    public class DllBaseTest
    {
        /// <summary>
        /// The rhino mocks repository
        /// </summary>
        protected MockRepository theMock;
        #region Mock objects
        protected ApplicationWorkflowStepElement stepElementMock;               // = theMock.PartialMock<ApplicationWorkflowStepElement>();
        protected ApplicationWorkflowStep stepMock;                             // = theMock.PartialMock<ApplicationWorkflowStep>();
        protected ApplicationWorkflowStepElementContent stepElementContentMock; // = theMock.PartialMock<ApplicationWorkflowStepElementContent>();
        protected PanelApplicationRepository thePanelApplicationRepositoryMock;
       
        #endregion
        #region Constants
        protected const int _goodUserId = 1111;
        protected const int _badUserId = -4;
        protected DateTime _noDate;
        protected int _notSet = 0;
        #endregion
        #region Attributes
        protected ApplicationWorkflowStepElement targetSourceStepElement;
        protected ApplicationWorkflowStepElement sourceSourceStepElement;
        #endregion
        #region Rhino Mock Wrappers
        /// <summary>
        /// Initialize all mocks object in the base.
        /// </summary>
        protected void InitializeMocks()
        {
            //
            // Create mock object
            //
            //
            // Now create the mocks
            //
            theMock = new MockRepository();
            //
            // finally set the controllers context
            //
            stepElementContentMock = theMock.PartialMock<ApplicationWorkflowStepElementContent>();
            stepMock = theMock.PartialMock<ApplicationWorkflowStep>();
            stepElementMock = theMock.PartialMock<ApplicationWorkflowStepElement>();
            thePanelApplicationRepositoryMock = theMock.PartialMock<PanelApplicationRepository>();
        }
        /// <summary>
        /// Clean up mocks after the test.
        /// </summary>
        protected void CleanUpMocks()
        {
            thePanelApplicationRepositoryMock = null;
            stepElementMock = null;
            stepMock = null;
            stepElementContentMock = null;
            theMock = null;
        }

        #endregion
        #region Helpers
        /// <summary>
        /// Verify's assumptions on IStandardDateFields objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">Expected user id</param>
        /// <remarks>
        /// Assumes Modified date was null to begin with.
        /// </remarks>
        protected void VerifyModifiedDate(IStandardDateFields entityObject, int userId)
        {
            Assert.AreEqual(userId, entityObject.ModifiedBy, "Modified by user id not set correctly");
            Assert.IsNotNull(entityObject.ModifiedDate, "Modified date not set correctly");
        }
        /// <summary>
        /// Verify's assumptions on IDateField objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">Expected user id</param>
        /// <remarks>
        /// Assumes Modified date was null to begin with.
        /// </remarks>
        protected void VerifyModifiedDate(IDateFields entityObject, int userId)
        {
            Assert.AreEqual(userId, entityObject.ModifiedBy, "Modified by user id not set correctly");
            Assert.AreNotEqual("1/1/0001", entityObject.ModifiedDate.ToShortDateString(), "Modified date not set correctly");
        }
        /// <summary>
        /// Verify's assumptions on IStandardDateFields objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <remarks>
        /// Assumes Create date & user were null to begin with.
        /// </remarks>
        protected void VerifyCreatedDateUnChanged(IStandardDateFields entityObject)
        {
            Assert.IsNull(entityObject.CreatedBy, "Created by user id not set correctly");
            Assert.IsNull(entityObject.CreatedDate, "Created date not set correctly");
        }
        /// <summary>
        /// Verify's assumptions on IStandardDateFields objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <remarks>
        /// Assumes Create date & user were null to begin with.
        /// </remarks>
        protected void VerifyModifiedDateUnChanged(IStandardDateFields entityObject)
        {
            Assert.IsNull(entityObject.ModifiedBy, "ModifiedBy user id not set correctly");
            Assert.IsNull(entityObject.ModifiedDate, "ModifiedDate not set correctly");
        }
        /// <summary>
        /// Verify's assumptions on IStandardDateFields objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <remarks>
        /// Assumes Create date & user were null to begin with.
        /// </remarks>
        protected void VerifyModifiedDateUnChanged(IDateFields entityObject)
        {
            Assert.AreEqual(0, entityObject.ModifiedBy, "ModifiedBy user id not set correctly");
            Assert.AreEqual("1/1/0001", entityObject.ModifiedDate.ToShortDateString(), "ModifiedDate not set correctly");
        }
        /// <summary>
        /// Verify's assumptions on IStandardDateFields objects
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <remarks>
        /// Assumes Create date & user were null to begin with.
        /// </remarks>
        protected void VerifyCreatedDateUnChanged(IDateFields entityObject)
        {
            Assert.AreEqual(0, entityObject.CreatedBy, "CreatedBy user id not set correctly");
            Assert.AreEqual("1/1/0001", entityObject.CreatedDate.ToShortDateString(), "CreatedDate not set correctly");
        }
        /// <summary>
        /// Generic helper method that constructs a ICollection of entity objects.
        /// </summary>
        /// <typeparam name="T">Concrete type of entity</typeparam>
        /// <param name="howMany">Number of entity to create</param>
        /// <returns>ICollection of entity objects</returns>
        public List<T> BuildEntityCollection<T>(int howMany) where T: new()
        {
            List<T> list = new List<T>();

            for (int i = 0; i < howMany; i++)
            {
                var x = new T();
                list.Add(x);
            }

            return list;
        }
        #endregion
    }
}
