using AutoMapper;
using Fiap.Api.Coletas.Controllers;
using Fiap.Api.Coletas.Data.Contexts;
using Fiap.Api.Coletas.Models;
using Fiap.Api.Coletas.Services;
using Fiap.Api.Coletas.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Api.Coletas.Test
{
    public class ColetasControllerTest
    {

        private readonly Mock<IColetasService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ColetasController _coletaController;

        public ColetasControllerTest()
        {
            _mockService = new Mock<IColetasService>();
            _mockMapper = new Mock<IMapper>();
            _coletaController = new ColetasController(_mockService.Object, _mockMapper.Object);
        }

        // Método para criar e configurar um DbSet mock para coletaModel
        private IQueryable<ColetasModel> GetTestColetas()
        {
            return new List<ColetasModel>
            {
                new ColetasModel { Id = 1, DataColeta = DateTime.Now },
                new ColetasModel { Id = 2, DataColeta = DateTime.Now }
            }.AsQueryable();
        }


        //-------------------------------TESTE coleta POR ID--------------------------------------------//
        //-----------------------------------STATUS CODE 200---------------------------------------------//
        [Fact]
        public void GetById_returnStatusCode200()
        {
            // Arrange
            long coletaId = 1;
            var coleta = new ColetasModel { Id = coletaId, DataColeta = DateTime.Now };
            var coletaViewModel = new ColetasViewModel { Id = coletaId, DataColeta = coleta.DataColeta };

            _mockService.Setup(service => service.ObtercoletaPorId(coletaId)).Returns(coleta);
            _mockMapper.Setup(mapper => mapper.Map<ColetasViewModel>(coleta)).Returns(coletaViewModel);

            // Act
            var result = _coletaController.Get(coletaId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ColetasViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);

            var returnValue = Assert.IsType<ColetasViewModel>(okResult.Value);
            Assert.Equal(coleta.Id, returnValue.Id);
            Assert.Equal(coleta.DataColeta, returnValue.DataColeta);
            // Adicione mais verificações conforme necessário para outras propriedades do ViewModel
        }
        //-----------------------------------------------------------------------------------------------//




        //----------------------------TESTE LISTAR Coletas PAGINADO---------------------------------------//
        //------------------------------------STATUS CODE 200----------------------------------------------//
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var Coletas = GetTestColetas().ToList();
            var coletaViewModel = Coletas.Select(a => new ColetasViewModel { Id = a.Id, DataColeta = a.DataColeta }).ToList();

            _mockService.Setup(service => service.ListarColetasReferencia(It.IsAny<int>(), It.IsAny<int>())).Returns(Coletas);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ColetasViewModel>>(It.IsAny<IEnumerable<ColetasModel>>())).Returns(coletaViewModel);

            // Act
            var result = _coletaController.Get(0, 10);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ColetasPaginacaoReferenciaViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);
            var returnValue = Assert.IsType<ColetasPaginacaoReferenciaViewModel>(okResult.Value);
            Assert.Equal(coletaViewModel.Count, returnValue.Coletas.Count());
        }

        //-----------------------------------------------------------------------------------------------//





        //--------------------------------TESTE CRIAR coleta--------------------------------------------//
        //-----------------------------------STATUS CODE 201---------------------------------------------//
        [Fact]
        public void Post_ReturnsStatusCode201()
        {
            // Arrange
            var coletaViewModel = new ColetasViewModel
            {
                DataColeta = DateTime.Now,
                HoraColeta = TimeSpan.FromHours(10),
                TipoResiduo = "Metais",
                Endereco = "Rua das flores, 100"
            };

            var coletaModel = new ColetasModel
            {
                Id = 1,
                DataColeta = coletaViewModel.DataColeta,
                HoraColeta = coletaViewModel.HoraColeta,
                TipoResiduo = coletaViewModel.TipoResiduo,
                Endereco = coletaViewModel.Endereco
            };

            _mockMapper.Setup(mapper => mapper.Map<ColetasModel>(coletaViewModel)).Returns(coletaModel);

            // Act
            var result = _coletaController.Post(coletaViewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//






        //-------------------------------TESTE ATUALIZAR coleta-----------------------------------------//
        //-----------------------------------STATUS CODE 204---------------------------------------------//
        [Fact]
        public void Put_ReturnsStatusCode204()
        {
            // Arrange
            var coletaId = 1;
            var coletaViewModel = new ColetasViewModel
            {
                Id = coletaId,
                DataColeta = DateTime.Now,
                HoraColeta = TimeSpan.FromHours(12),
                TipoResiduo = "Orgânico",
                Endereco = "Rua Liberdade, 123"
            };

            var coletaModel = new ColetasModel
            {
                Id = coletaId,
                DataColeta = coletaViewModel.DataColeta,
                HoraColeta = coletaViewModel.HoraColeta,
                TipoResiduo = coletaViewModel.TipoResiduo,
                Endereco = coletaViewModel.Endereco
            };

            _mockService.Setup(service => service.ObtercoletaPorId(coletaId)).Returns(coletaModel);

            // Act
            var result = _coletaController.Put(coletaId, coletaViewModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//






        //--------------------------------TESTE EXCLUIR coleta------------------------------------------//
        //-----------------------------------STATUS CODE 204---------------------------------------------//
        [Fact]
        public void Delete_ReturnsStatusCode204()
        {
            // Arrange
            var coletaId = 1;

            _mockService.Setup(service => service.ObtercoletaPorId(coletaId)).Returns(new ColetasModel { Id = coletaId });

            // Act
            var result = _coletaController.Delete(coletaId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
        //-----------------------------------------------------------------------------------------------//










    }
}
