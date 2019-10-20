import React from "react";
import { ChartBar } from "./ChartBar";
import CountiesSelect from "./CountiesSelect";
import { FormGroup, Col, Button } from "reactstrap";
const dataFromServer = [
  {
    id: "1",
    name: "Candidate X",
    votes: 1209484,
    url: "",
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
    <div style={{ width: "700px", height: "500px" }}>
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
          {!showAll ? <Button onClick={toggleShowAll}>Show more</Button> : null}
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
