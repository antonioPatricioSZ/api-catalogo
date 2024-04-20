using APICatalogo.Communication.Requests;
using APICatalogo.Domain.Repositories.UnitOfWork;
using APICatalogo.Exceptions;
using AutoMapper;

namespace APICatalogo.Application.UseCases.Product.Update;

public class UpdateProductUseCase : IUpdateProductUseCase {

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductUseCase(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(int id, ProductRequestJson requestDTO) {

        var produto = await _unitOfWork.ProdutoRepository.GetUpdate(
            p => p.Id == id
        ) ?? throw new NotFoundException("Produto não encontrado.");

        _mapper.Map(requestDTO, produto);

        _unitOfWork.ProdutoRepository.Update(produto);
        await _unitOfWork.Commit();

    }

}
