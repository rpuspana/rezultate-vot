import React from "react";
import { ChartBar } from "./ChartBar";
const dataFromServer = [
  {
    id: "1",
    name: "Candidate X",
    votes: 1209484,
    url:
      "https://blog.mycase.com/wp-content/uploads/2015/09/rule-of-thumb-589x505.png",
    percent: 60
  },
  {
    id: "2",
    name: "Candidate X",
    votes: 1209484,
    url:
      "https://blog.mycase.com/wp-content/uploads/2015/09/rule-of-thumb-589x505.png",
    percent: 25
  },
  {
    id: "3",
    name: "Candidate X",
    votes: 1209484,
    url:
      "https://blog.mycase.com/wp-content/uploads/2015/09/rule-of-thumb-589x505.png",
    percent: 40
  }
];

const getBarChart = () => {
  dataFromServer.map((candidate, index) => (
    <ChartBar
      key={candidate.id}
      percent={candidate.percent}
      imgUrl={candidate.url}
      candidateName={candidate.name}
      votesNumber={candidate.votes}
    />
  ));
};

export const ChartContainer = () => {
  const [showMore, toggleShowMore] = React.useState(false);
  return (
    <div style={{ width: "700px", height: "500px" }}>
      {dataFromServer ? (
        <>
          {getBarChart()}
          <button>Show more</button>
        </>
      ) : (
        <div>
          <div>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua.
              Viverra nam libero justo laoreet sit amet cursus sit. Malesuada
              nunc vel risus commodo viverra maecenas.
            </p>
          </div>
          <div>Cine sunt candidatii care merg in turul 2?</div>
        </div>
      )}
    </div>
  );
};
