using System.Collections.Generic;
using TestClass = NUnit.Framework.TestFixtureAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit test for SummaryManagementService - StartApplications
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest
    {
        private List<string> _multipleApplicationIds = new List<string>() { "AppId-1", "AppId-2", "AppId-3", "AppId-4" };
        private List<string> _oneApplicationIds = new List<string>() { "AppId-1" };
        private List<string> _emptyApplicationIds = new List<string>();

        //TODO: update these unit tests to new signature
        //#region Start Applications Tests
        ///// <summary>
        ///// Test setting a several review status
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void GoodStartApplicationStateSingleTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Set up the expectations & results to be returned
        //    //
        //    SetupResult.For(unitOfWorkMock.ApplicationRepository).Return(repositoryMock);
        //    Expect.Call(delegate { repositoryMock.StartApplications(_oneApplicationIds, _userId); });
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(_oneApplicationIds, _userId);
        //    //
        //    // Other than executing all of the methods there really is nothing
        //    // that the service layer changes.
        //    //
        //    mocks.VerifyAll();
        //}
        ///// <summary>
        ///// Test setting a several review status
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void GoodStartApplicationStateMultipleTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Set up the expectations & results to be returned
        //    //
        //    SetupResult.For(unitOfWorkMock.ApplicationRepository).Return(repositoryMock);
        //    Expect.Call(delegate { repositoryMock.StartApplications(_multipleApplicationIds, _userId); });
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(_multipleApplicationIds, _userId);
        //    //
        //    // Other than executing all of the methods there really is nothing
        //    // that the service layer changes.
        //    //
        //    mocks.VerifyAll();
        //}

        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_NullApplicationIdsTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(null, _userId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //}

        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_NullApplicationIdsBadUserIdTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(null, _badUserId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //}
        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_NullApplicationIdsZeroUserIdTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(null, _zeroUserId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //}
        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_EmptyApplicationIdsTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(_emptyApplicationIds, _userId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //}
        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_EmptyApplicationIdsBadUserIdTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(_emptyApplicationIds, _badUserId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //}
        ///// <summary>
        ///// Test null application collection
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementServer.StartApplication")]
        //public void StartApplicationStateSingle_EmptyApplicationIdsZeroUserIdTest()
        //{
        //    //
        //    // Create the mocks
        //    //
        //    MockRepository mocks = new MockRepository();
        //    IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
        //    IApplicationRepository repositoryMock = mocks.DynamicMock<IApplicationRepository>();
        //    //
        //    // Create the service; turn off recording & test
        //    //
        //    ISummaryManagementService serviceMock = mocks.PartialMock<TestSummaryManagementService>(unitOfWorkMock);
        //    mocks.ReplayAll();
        //    serviceMock.StartApplications(_emptyApplicationIds, _zeroUserId);
        //    //
        //    // Make sure that Repository method was not called.
        //    // 
        //    repositoryMock.AssertWasNotCalled(x => x.StartApplications(Arg<List<string>>.Is.Anything, Arg<int>.Is.Anything));
        //    unitOfWorkMock.VerifyAllExpectations();
        //} 
        //#endregion
    }
}
