import React from "react";
import { ChartBar } from "./ChartBar";
import CountiesSelect from "./CountiesSelect";
import { FormGroup, Col, Button } from "reactstrap";
const dataFromServer = [
  {
    id: "1",
    name: "Candidate X",
    votes: 1209484,
    url:
      "https://us.123rf.com/450wm/kritchanut/kritchanut1406/kritchanut140600112/29213222-stock-vector-male-silhouette-avatar-profile-picture.jpg?ver=6&fbclid=IwAR0HzpXjYDnMwHFfD8fdnNFrBa8rmLJ74i3NbOsPZvzwNNY7WANjuX6DvGA",
    percent: 60
  },
  {
    id: "2",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 25
  },
  {
    id: "3",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 40
  },
  {
    id: "4",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 60
  },
  {
    id: "5",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 25
  },
  {
    id: "6",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 40
  },
  {
    id: "7",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 60
  },
  {
    id: "8",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 25
  },
  {
    id: "9",
    name: "Candidate X",
    votes: 1209484,
    url: "",
    percent: 40
  }
];

export const ChartContainer = () => {
  const [showAll, toggleShowAll] = React.useState(false);

  return (
    <div>
      <div sm={12} className={"votes-container"}>
        <p className={"votes-container-text"}>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Viverra
          nam libero justo laoreet sit amet cursus sit. Malesuada nunc vel risus
          commodo viverra maecenas.
        </p>
        <div sm={3} className={"votes-numbers"}>
          <h3 className={"votes-title"}>Voturi numarate</h3>
          <div sm={3} className={"votes-results"}>
            <p className={"votes-percent"}>30%</p>
            <p className={"votes-text"}>12.400.000</p>
          </div>
        </div>
      </div>
      <FormGroup row>
        <Col sm={3}>
          <CountiesSelect />
        </Col>
      </FormGroup>
      {dataFromServer ? (
        <div>
          {(showAll ? dataFromServer : dataFromServer.slice(0, 5)).map(
            candidate => (
              <ChartBar
                key={candidate.id}
                percent={candidate.percent}
                imgUrl={candidate.url}
                candidateName={candidate.name}
                votesNumber={candidate.votes}
              />
            )
          )}
          {!showAll ? (
            <div className={"show-all-container"} sm={3}>
              <Button className={"show-all-btn"} onClick={toggleShowAll}>
                Afiseaza toti candidatii
              </Button>
            </div>
          ) : null}
        </div>
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
