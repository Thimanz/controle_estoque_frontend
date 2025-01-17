import { useEffect, useState, useRef } from "react";
import { FaCaretDown, FaCaretUp, FaTrash, FaUpload } from "react-icons/fa";
import { useNavigate, useParams } from "react-router-dom";
import {
    deleteProduct,
    getProduct,
    updateProduct,
} from "../../services/productsService";
import { getCategoryList } from "../../services/categoryService";
import { ToastContainer, toast, Bounce } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./ProductPage.css";

const ProductPage = () => {
    const productId = useParams().id;
    const categoryRef = useRef();
    const navigate = useNavigate();

    const [product, setProduct] = useState({});
    const [categories, setCategories] = useState([]);

    const [categoryDropdownActive, setCategoryDropdownActive] = useState(false);
    const [categoryId, setCategoryId] = useState("");
    const [category, setCategory] = useState("");
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [barcode, setBarcode] = useState("");
    const [costPrice, setCostPrice] = useState("");
    const [sellingPrice, setSellingPrice] = useState("");
    const [minInStock, setMinInStock] = useState("");
    const [length, setLength] = useState("");
    const [width, setWidth] = useState("");
    const [height, setHeight] = useState("");
    const [weight, setWeight] = useState("");
    const [image, setImage] = useState(null);

    useEffect(() => {
        const fetchProduct = async () => {
            const response = await getProduct(productId, navigate);
            if (response.status === 200) {
                setProduct(response.data);
                setCategoryId(response.data.categoria.id);
                setCategory(response.data.categoria.nome);
                setName(response.data.nome);
                setDescription(response.data.descricao);
                setBarcode(response.data.codigoBarras);
                setCostPrice(response.data.precoCusto);
                setSellingPrice(response.data.precoVenda);
                setMinInStock(response.data.nivelMinimoEstoque);
                setLength(response.data.comprimento);
                setWidth(response.data.largura);
                setHeight(response.data.altura);
                setWeight(response.data.peso);
                setImage(response.data.imagem);
            } else {
                navigate("/not-found");
            }
        };

        const fetchCategories = async () => {
            const categoriesData = await getCategoryList(navigate);
            setCategories(categoriesData.data);
        };

        fetchProduct();
        fetchCategories();
    }, [productId]);

    const updateButton = async () => {
        const response = await updateProduct(
            productId,
            {
                nome: name,
                descricao: description,
                codigoBarras: barcode,
                categoriaId: categoryId,
                precoCusto: costPrice,
                precoVenda: sellingPrice,
                imagem: image,
                nivelMinimoEstoque: parseInt(minInStock),
                comprimento: parseFloat(length),
                largura: parseFloat(width),
                altura: parseFloat(height),
                peso: parseFloat(weight),
            },
            navigate
        );
        if (response.status === 204) {
            navigate("/inicio", {
                state: {
                    successMsg: "Produto Atualizado com Sucesso",
                    tab: "Produtos",
                },
            });
        } else {
            response.errors.mensagens.forEach((mensagem) => {
                toast.error(mensagem, {
                    position: "top-center",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: false,
                    pauseOnHover: false,
                    draggable: true,
                    progress: undefined,
                    theme: "colored",
                    transition: Bounce,
                });
            });
        }
    };

    const deleteButton = async () => {
        const response = await deleteProduct(productId, navigate);
        if (response.status === 204) {
            navigate("/inicio", {
                state: {
                    successMsg: "Produto Deletado com Sucesso",
                    tab: "Produtos",
                },
            });
        } else {
            response.errors.mensagens.forEach((mensagem) => {
                toast.error(mensagem, {
                    position: "top-center",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: false,
                    pauseOnHover: false,
                    draggable: true,
                    progress: undefined,
                    theme: "colored",
                    transition: Bounce,
                });
            });
        }
    };

    const loadImage = (e) => {
        const fileReader = new FileReader();

        fileReader.onloadend = () => {
            setImage(fileReader.result);
        };

        if (e.target.files[0]) fileReader.readAsDataURL(e.target.files[0]);
    };

    return (
        <main className="main-product-page">
            <ToastContainer />
            <div className="product-info-image">
                <section className="product-image-container">
                    <img
                        src={product.imagem}
                        alt="imagem do produto"
                        className="product-image-L"
                    />
                </section>
                <section className="product-info">
                    <div className="product-forms">
                        <section className="left-product-form">
                            <div className="product-input-group">
                                <input
                                    type="text"
                                    onChange={(e) => {
                                        setName(e.target.value);
                                    }}
                                    required
                                    value={name}
                                    autoComplete="off"
                                />
                                <label htmlFor="nome">Nome</label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="text"
                                    onChange={(e) => {
                                        setBarcode(e.target.value);
                                    }}
                                    required
                                    value={barcode}
                                    autoComplete="off"
                                />
                                <label htmlFor="codigoBarras">
                                    Código de Barras
                                </label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setCostPrice(e.target.value);
                                    }}
                                    required
                                    value={costPrice}
                                    autoComplete="off"
                                />
                                <label htmlFor="precoCusto">
                                    Preço de custo
                                </label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setMinInStock(e.target.value);
                                    }}
                                    required
                                    value={minInStock}
                                    autoComplete="off"
                                />
                                <label htmlFor="nivelMinimoEstoque">
                                    Nivel Minimo no Estoque
                                </label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setWidth(e.target.value);
                                    }}
                                    required
                                    value={width}
                                    autoComplete="off"
                                />
                                <label htmlFor="largura">Largura (cm)</label>
                            </div>
                        </section>
                        <section className="right-product-form">
                            <div className="product-input-group dropdown">
                                <div
                                    onClick={(e) => {
                                        setCategoryDropdownActive(
                                            !categoryDropdownActive
                                        );
                                        categoryRef.current.focus();
                                    }}
                                >
                                    <input
                                        ref={categoryRef}
                                        type="button"
                                        value={category}
                                        required
                                        className="dropdown-btn"
                                    />
                                    <label htmlFor="categoria">Categoria</label>
                                    {categoryDropdownActive ? (
                                        <FaCaretUp className="arrow-icon" />
                                    ) : (
                                        <FaCaretDown className="arrow-icon" />
                                    )}
                                </div>
                                <div
                                    className="dropdown-content"
                                    style={{
                                        display: categoryDropdownActive
                                            ? "block"
                                            : "none",
                                    }}
                                >
                                    {categories.map((category) => {
                                        return (
                                            <div
                                                key={category.id}
                                                onClick={(e) => {
                                                    setCategory(
                                                        e.target.textContent
                                                    );
                                                    setCategoryDropdownActive(
                                                        !categoryDropdownActive
                                                    );
                                                    setCategoryId(category.id);
                                                }}
                                                className="item"
                                            >
                                                {category.nome}
                                            </div>
                                        );
                                    })}
                                </div>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setSellingPrice(e.target.value);
                                    }}
                                    required
                                    value={sellingPrice}
                                    autoComplete="off"
                                />
                                <label htmlFor="precoVenda">
                                    Preço de venda
                                </label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setWeight(e.target.value);
                                    }}
                                    required
                                    value={weight}
                                    autoComplete="off"
                                />
                                <label htmlFor="peso">Peso (kg)</label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setLength(e.target.value);
                                    }}
                                    required
                                    value={length}
                                    autoComplete="off"
                                />
                                <label htmlFor="comprimento">
                                    Comprimento (cm)
                                </label>
                            </div>
                            <div className="product-input-group">
                                <input
                                    type="number"
                                    pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                    step="any"
                                    onChange={(e) => {
                                        setHeight(e.target.value);
                                    }}
                                    required
                                    value={height}
                                    autoComplete="off"
                                />
                                <label htmlFor="altura">Altura (cm)</label>
                            </div>
                        </section>
                    </div>
                    <div className="product-input-group margin-0-override">
                        <input
                            accept="image/*"
                            aria-label="Imagem"
                            type="file"
                            required
                            autoComplete="off"
                            onChange={loadImage}
                        />
                        <label htmlFor="Imagem" className="image-input-label">
                            <FaUpload />
                            {" Selecione uma Imagem"}
                        </label>
                    </div>
                    <div className="product-input-group">
                        <input
                            type="text"
                            onChange={(e) => {
                                setDescription(e.target.value);
                            }}
                            required
                            value={description}
                            autoComplete="off"
                        />
                        <label htmlFor="descricao">Descrição</label>
                    </div>
                </section>
            </div>
            <div className="product-buttons">
                <button className="delete-product" onClick={deleteButton}>
                    Deletar
                </button>
                <button className="update-product" onClick={updateButton}>
                    Atualizar
                </button>
            </div>
        </main>
    );
};

export default ProductPage;
