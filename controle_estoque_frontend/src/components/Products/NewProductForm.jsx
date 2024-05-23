import { FaCaretDown, FaCaretUp } from "react-icons/fa";
import "./NewProductForm.css";
import { useEffect, useRef, useState } from "react";
import { getCategoryList } from "../../services/categoryService";
import { useNavigate } from "react-router-dom";
import { postProduct } from "../../services/productsService";
import { ToastContainer, toast, Bounce } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const NewProductForm = () => {
    const categoryRef = useRef();
    const navigate = useNavigate();

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

    const [categories, setCategories] = useState([]);

    useEffect(() => {
        const fetchCategories = async () => {
            const categoriesData = await getCategoryList(navigate);
            setCategories(categoriesData.data);
        };
        fetchCategories();
    }, []);

    const sendProduct = async () => {
        const response = await postProduct(
            {
                nome: name,
                descricao: description,
                codigoBarras: barcode,
                categoriaId: categoryId,
                precoCusto: costPrice,
                precoVenda: sellingPrice,
                imagem: null,
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
                    successMsg: "Produto Cadastrado com Sucesso",
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

    return (
        <main className="main-product">
            <ToastContainer />
            <section className="centered-block">
                <div className="product-forms">
                    <section className="left-product-form">
                        <div className="input-group">
                            <input
                                type="text"
                                onBlur={(e) => {
                                    setName(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="nome">Nome</label>
                        </div>
                        <div className="input-group">
                            <input
                                type="text"
                                onBlur={(e) => {
                                    setBarcode(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="codigoBarras">
                                Código de Barras
                            </label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setCostPrice(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="precoCusto">Preço de custo</label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setMinInStock(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="nivelMinimoEstoque">
                                Nivel Minimo no Estoque
                            </label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setWidth(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="largura">Largura (cm)</label>
                        </div>
                    </section>
                    <section className="right-product-form">
                        <div className="input-group dropdown">
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
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setSellingPrice(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="precoVenda">Preço de venda</label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setWeight(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="peso">Peso (kg)</label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setLength(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="comprimento">
                                Comprimento (cm)
                            </label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setHeight(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="altura">Altura (cm)</label>
                        </div>
                    </section>
                </div>
                <div className="input-group">
                    <input
                        type="text"
                        onBlur={(e) => {
                            setDescription(e.target.value);
                        }}
                        required
                        autoComplete="off"
                    />
                    <label htmlFor="descricao">Descrição</label>
                </div>
                <button className="confirm-product" onClick={sendProduct}>
                    Cadastrar Produto
                </button>
            </section>
        </main>
    );
};

export default NewProductForm;
