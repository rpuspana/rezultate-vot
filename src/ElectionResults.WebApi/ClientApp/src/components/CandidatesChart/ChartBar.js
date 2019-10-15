import React from "react";
import "./styles.css";

export const ChartBar = ({ percent, imgUrl, candidateName, votesNumber }) => {
  return (
    <div className={"bar-container"}>
      <div className={"bar-icon"}>
        <img src={imgUrl} />
      </div>
      <div className={"bar-candidate"}>
        <p style={{ fontSize: "28px" }}>{candidateName}</p>
      </div>
      <div className={"bar-results"}>
        <div
          className={"bar-votes"}
          style={{
            width: `${percent}%`
          }}
        >
          <div className={"bar-votes-text"}>
            <p style={{ fontSize: "35px" }}>{`${percent}%`}</p>
            <p style={{ fontSize: "18px" }}>{votesNumber}</p>
          </div>
        </div>
      </div>
    </div>
  );
};
