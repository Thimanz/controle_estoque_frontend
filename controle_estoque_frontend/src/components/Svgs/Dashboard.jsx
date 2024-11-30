import React from "react";

const Dashboard = ({ height, width }) => {
    return (
        <svg
            fill="#000000"
            viewBox="0 0 24 24"
            height={height}
            width={width}
            id="dashboard-alt-2"
            xmlns="http://www.w3.org/2000/svg"
            className="icon multi-color"
        >
            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
            <g
                id="SVGRepo_tracerCarrier"
                strokeLinecap="round"
                strokeLinejoin="round"
            ></g>
            <g id="SVGRepo_iconCarrier">
                <path
                    id="secondary-fill"
                    d="M20.74,14.48A8.89,8.89,0,0,1,17.51,20H6.23A8.89,8.89,0,0,1,3,14.48a9,9,0,0,1,17.74,0Z"
                    style={{ fill: "#2ca9bc", strokeWidth: 2 }}
                ></path>
                <path
                    id="primary-stroke"
                    d="M21,13a9,9,0,0,1-3.36,7H6.36A9,9,0,1,1,21,13Zm-9,1a1,1,0,1,0,1,1A1,1,0,0,0,12,14Zm4-5-4,6"
                    style={{
                        fill: "none",
                        stroke: "#000000",
                        strokeLinecap: "round",
                        strokeLinejoin: "round",
                        strokeWidth: 2,
                    }}
                ></path>
            </g>
        </svg>
    );
};

export default Dashboard;
