using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
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
    private HomeController _controller;
    
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IWordRepository>();
        _mockRepository.Setup(repo => repo.ReturnWordListFromSource()).Returns(new List<Word>());
        _anagramSolver = new AnagramSolver.BusinessLogic.AnagramSolver(_mockRepository.Object);
        _controller = new HomeController(_anagramSolver);
    }

    [Test]
    public void Empty_ShouldWork_ReturnOKStatusCode()
    {
        var result = _controller.Empty();
        // Assert
        var redirectToViewResult = result.ShouldBeOfType<ViewResult>();
        redirectToViewResult.ViewName.ShouldBe("Empty");

    }
    // Noriu patikrinti kaip patikrinti kokį View grąžins jei url bus ~/ arba ~/zodis.
    // Pirmu atveju turi grąžinti View "Empty", kitu atveju "Index".
    // Niekur neradau kaip galiu testuoti url ir pagal tai patikrinti grąžinamus View.
    [Test]
    public void Index_ShouldWork_ReturnsIndexView()
    {
        var result = _controller.Index("zodis");
        var viewResult = result.ShouldBeOfType<ViewResult>();
        viewResult.ViewName.ShouldBe("Index");
        // šitoj vietoj nesuprantu kodėl testas rašo kad yra "aborted", nors pagal rezultatą gaunasi pavykęs.
    }

    [Test]
    public void ViewAll_ShouldWork_ReturnsVieAllView()
    {
        var result = _controller.ViewAll(pageNumber: null);
        var viewResult = result.Result.ShouldBeOfType<ViewResult>();
        viewResult.Model.ShouldBeOfType<PaginatedList<Word>>();
    }
}