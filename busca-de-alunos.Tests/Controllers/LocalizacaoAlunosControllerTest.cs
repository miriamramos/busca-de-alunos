using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using busca_de_alunos.Models;
using busca_de_alunos.Controllers;


namespace busca_de_alunos.Tests.Controllers
{
    [TestClass]
    public class LocalizacaoAlunosControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePost()
        {
            var localizacaoAluno = new LocalizacaoAluno()
            {
                Matricula = "MAT001",
                Nome = "Teste",
                Endereco = "Teste"
            };

            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();
            
            // Act.
            var result = controller.Create(localizacaoAluno) as ActionResult;

            // Assert.
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();

            // Act
            ViewResult result = controller.Edit("MAT001") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditPost()
        {
            var localizacaoAluno = new LocalizacaoAluno()
            {
                Matricula = "MAT001",
                Nome = "Teste1",
                Endereco = "Teste"
            };

            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();

            // Act.
            var result = controller.Edit(localizacaoAluno) as ActionResult;

            // Assert.
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            LocalizacaoAlunosController controller = new LocalizacaoAlunosController();

            // Act
            ActionResult result = controller.Delete("MAT001") as ActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}
