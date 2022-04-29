using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using WebApp.Controllers;

namespace WebApp.Tests;

[TestFixture]
public class HomeControllerTests
{
    private AnagramSolver.BusinessLogic.AnagramSolver _anagramSolver;
    private Mock<IWordRepository> _mockRepository;
    private Mock<ICache> _mockCache;
    private HomeController _controller;
    
    [SetUp]
    public void Setup(DataContext _context)
    {
        _mockRepository = new Mock<IWordRepository>();
        _mockCache = new Mock<ICache>();
        _mockRepository.Setup(repo => repo.ReturnWordListFromSource(true)).Returns(new List<Words>());
        _anagramSolver = new AnagramSolver.BusinessLogic.AnagramSolver(_mockRepository.Object, _mockCache.Object);
        _controller = new HomeController(_anagramSolver, _context);
    }

    [Test]
    public void Empty_ShouldWork_ReturnOKStatusCode()
    {
        var result = _controller.Empty();
        // Assert
        var redirectToViewResult = result.ShouldBeOfType<ViewResult>();
        redirectToViewResult.ViewName.ShouldBe("Empty");
    }

    [Test]
    public void Index_ShouldWork_ReturnsIndexView()
    {
        var result = _controller.Index("zodis");
        var viewResult = result.ShouldBeOfType<ViewResult>();
        viewResult.ViewName.ShouldBe("Index");
    }

    [Test]
    public void ViewAll_ShouldWork_ReturnsVieAllView()
    {
        var result = _controller.ViewAll(pageNumber: null);
        var viewResult = result.Result.ShouldBeOfType<ViewResult>();
        viewResult.Model.ShouldBeOfType<PaginatedList<Words>>();
    }
}