import React, { useState, useEffect } from "react";
// import axios from "axios";
// import Table from "@mui/material/Table";
// import TableBody from "@mui/material/TableBody";
// import TableCell from "@mui/material/TableCell";
// import TableContainer from "@mui/material/TableContainer";
// import TableHead from "@mui/material/TableHead";
// import TableRow from "@mui/material/TableRow";
// import Paper from "@mui/material/Paper";
// import Button from "@mui/material/Button";
// import TextField from "@mui/material/TextField";
// import Checkbox from "@mui/material/Checkbox";
// import FormControlLabel from "@mui/material/FormControlLabel";
// import InputLabel from "@mui/material/InputLabel";
// import MenuItem from "@mui/material/MenuItem";
// import FormControl from "@mui/material/FormControl";
// import Select from "@mui/material/Select";
// import Dialog from "@mui/material/Dialog";

// import EditGroup from "components/groups/administration/EditGroup";
// import ChooseSubscription from "components/groups/administration/ChooseSubscription";

import { Link } from "react-router-dom";

const AdminGroupUsersBrowser = () => {
  //group administration
  //   const [open, setOpen] = React.useState(false);
  //   const [editedGroup, setEditedGroup] = React.useState(false);

  //   //packets administration
  //   const [subscriptionOpen, setSubscriptionOpen] = React.useState(false);
  //   const [editedSubscription, setEditedSubscription] = React.useState(null);
  //   const [subscriptionGroup, setSubscriptionGroup] = React.useState(null);

  //   // filters
  //   const [groups, setGroups] = useState([]);
  //   const [topic, setTopic] = useState(null);
  //   const [name, setName] = useState("");
  //   const [isOpen, setIsOpen] = useState(false);
  //   const [topics, setTopics] = useState(["Sport", "Polityka", "Turystyka"]);

  //   const handleClickOpen = () => {
  //     setOpen(true);
  //   };

  //   const handleClose = () => {
  //     setOpen(false);
  //   };

  //   const handleSubscriptionClickOpen = () => {
  //     setSubscriptionOpen(true);
  //   };

  //   const handleSubscriptionClose = () => {
  //     setSubscriptionOpen(false);
  //   };

  //   const fetchGroups = async () => {
  //     const result = await axios.post("/api/Groups/my", {
  //       IsMember: true,
  //       isOwner: true,
  //       topic: topic,
  //       name: name,
  //       isOpen: isOpen,
  //     });

  //     if (result && result.data && result.data.success) {
  //       console.log(result.data.data);
  //       setGroups(result.data.data);
  //     }
  //   };

  //   useEffect(() => {
  //     fetchGroups();
  //   }, [name, topic, isOpen]);

  return (
    <>
      <div className="container d-flex justify-content-center">
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          <div className="row mx-4 mb-4 align-items-center">
            <div className="row align-items-center">
              <Link to={`/groups/administration`}>
                <button
                  type="button"
                  className="btn ml-2 btn-outline-primary rounded-circle btn-sm"
                  onClick={() => {}}
                >
                  <i class="fa fa-arrow-left" aria-hidden="true"></i>
                </button>
              </Link>

              <h6 className="mx-2 mt-1">Powrót</h6>
            </div>
          </div>
        </div>
      </div>
    </>
    // <>
    //   <Dialog open={open} onClose={handleClose}>
    //     <EditGroup
    //       group={editedGroup}
    //       setGroup={setEditedGroup}
    //       onCancel={handleClose}
    //       onSuccess={() => {
    //         fetchGroups();
    //         handleClose();
    //       }}
    //     />
    //   </Dialog>

    //   <Dialog open={subscriptionOpen} onClose={handleSubscriptionClose}>
    //     <ChooseSubscription
    //       groupId={subscriptionGroup}
    //       subscription={editedSubscription}
    //       setSubscription={setEditedSubscription}
    //       onCancel={handleSubscriptionClose}
    //       onSuccess={() => {
    //         fetchGroups();
    //         handleSubscriptionClose();
    //       }}
    //     />
    //   </Dialog>

    //   <div className="container d-flex justify-content-center">
    //     <div
    //       className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
    //       style={{ border: "#8f8f8fb6" }}
    //     >
    //       <div className="row mx-4 mb-4 align-items-center">
    //         <div style={{ width: "50px" }}>
    //           <button
    //             type="button"
    //             class="btn btn-primary rounded-circle"
    //             onClick={() => {
    //               setEditedGroup({});
    //               handleClickOpen();
    //             }}
    //           >
    //             <i class="fa fa-plus" aria-hidden="true"></i>
    //           </button>
    //         </div>

    //         <div style={{ width: "250px" }}>
    //           <FormControl fullWidth>
    //             <InputLabel id="demo-simple-select-label">Temat</InputLabel>
    //             <Select
    //               labelId="demo-simple-select-label"
    //               id="demo-simple-select"
    //               value={topic}
    //               label="Temat"
    //               onChange={(e) => {
    //                 setTopic(e.target.value);
    //               }}
    //             >
    //               {topics.map((t) => {
    //                 return (
    //                   <MenuItem key={t} value={t}>
    //                     {t}
    //                   </MenuItem>
    //                 );
    //               })}
    //             </Select>
    //           </FormControl>
    //         </div>
    //         <div className="mx-2" style={{ width: "300px" }}>
    //           <TextField
    //             fullWidth
    //             value={name}
    //             id="outlined-basic"
    //             label="Nazwa"
    //             variant="outlined"
    //             onChange={(e) => {
    //               setName(e.target.value);
    //             }}
    //           />
    //         </div>

    //         <FormControlLabel
    //           className="mx-4 mt-2"
    //           control={
    //             <Checkbox
    //               checked={isOpen}
    //               onChange={(e) => {
    //                 setIsOpen(e.target.checked);
    //               }}
    //             />
    //           }
    //           label="Grupy otwarte"
    //         />
    //       </div>
    //       <TableContainer component={Paper}>
    //         <Table aria-label="simple table">
    //           <TableHead>
    //             <TableRow>
    //               <TableCell width="200px"></TableCell>
    //               <TableCell align="left">Nazwa</TableCell>

    //               <TableCell width="200px" align="center">
    //                 Pakiet
    //               </TableCell>
    //               <TableCell width="30px" align="center"></TableCell>
    //               <TableCell width="200px" align="center">
    //                 Liczba członków
    //               </TableCell>
    //               <TableCell width="30px" align="center"></TableCell>
    //               <TableCell width="200px" align="center">
    //                 Limit osób
    //               </TableCell>
    //               <TableCell width="200px" align="center">
    //                 Czy grupa otwarta?
    //               </TableCell>
    //             </TableRow>
    //           </TableHead>
    //           <TableBody>
    //             {groups.map((row) => (
    //               <TableRow
    //                 key={row.name}
    //                 sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
    //               >
    //                 <TableCell align="center">
    //                   <Button
    //                     variant="contained"
    //                     onClick={() => {
    //                       setEditedGroup({ ...row });
    //                       setOpen(true);
    //                     }}
    //                   >
    //                     Edytuj
    //                   </Button>
    //                 </TableCell>
    //                 <TableCell align="left">{row.name}</TableCell>
    //                 <TableCell align="center">
    //                   {row.packetName}{" "}
    //                   {!row.packetName && (
    //                     <button
    //                       type="button"
    //                       class="btn btn-outline-primary rounded-circle btn-sm"
    //                       onClick={() => {
    //                         setEditedSubscription({});
    //                         setSubscriptionGroup(row.id);
    //                         handleSubscriptionClickOpen();
    //                       }}
    //                     >
    //                       <i class="fa fa-plus" aria-hidden="true"></i>
    //                     </button>
    //                   )}
    //                 </TableCell>
    //                 <TableCell>
    //                   {row.packetName && (
    //                     <button
    //                       type="button"
    //                       className="btn ml-2 btn-outline-primary rounded-circle btn-sm"
    //                       onClick={() => {
    //                         setEditedSubscription({
    //                           subscriptionId: row.subscriptionId,
    //                         });
    //                         setSubscriptionGroup(row.id);
    //                         handleSubscriptionClickOpen();
    //                       }}
    //                     >
    //                       <i className="fa fa-pencil" aria-hidden="true"></i>
    //                     </button>
    //                   )}
    //                 </TableCell>
    //                 <TableCell align="center">{row.membersCount}</TableCell>
    //                 <TableCell align="center">
    //                   <Link to={`/groups/administration/${row.id}/users`}>
    //                   <button
    //                     type="button"
    //                     className="btn ml-2 btn-outline-primary rounded-circle btn-sm"
    //                     onClick={() => {}}
    //                   >
    //                     <i class="fa fa-list" aria-hidden="true"></i>
    //                   </button>
    //                   </Link>
    //                 </TableCell>
    //                 <TableCell align="center">
    //                   {row.packetPeopleLimit}
    //                 </TableCell>
    //                 <TableCell align="center">
    //                   {row.isOpen ? "Tak" : "Nie"}
    //                 </TableCell>
    //               </TableRow>
    //             ))}
    //           </TableBody>
    //         </Table>
    //       </TableContainer>
    //     </div>
    //   </div>
    // </>
  );
};
export default AdminGroupUsersBrowser;
