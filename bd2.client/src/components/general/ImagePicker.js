import React from "react";
import { useDropzone } from "react-dropzone";

// Note that there will be nothing logged when files are dropped
const ImagePicker = ({
  files,
  setFiles,
  style = { width: "100%", height: "100px" },
  className = "",
}) => {
  const { getRootProps, getInputProps } = useDropzone({
    onDrop: (newFiles) => setFiles([...files, ...newFiles]),
    accept: "image/*",
  });

  return (
    <>
      <div className={className} style={style}>
        <div
          style={{ width: "100%", height: "100%" }}
          {...getRootProps({
            className: "dropzone",
          })}
        >
          <input {...getInputProps()} />
          <div>Naciśnij lub upuść zdjęcia</div>
        </div>
      </div>

      <div className="images-panel">
        {files.map((x, i) => (
          <div>
            <i
              class="fa fa-times-circle image-exit-icon"
              aria-hidden="true"
              onClick={() =>
                setFiles(files.filter((file, index) => index !== i))
              }
            ></i>
            <img src={URL.createObjectURL(x)} className="image-object" alt="obraz" />
          </div>
        ))}
      </div>
    </>
  );
};

export default ImagePicker;

/*
import React, { useState } from "react";
import { useDropzone } from "react-dropzone";

function ImagePicker(props) {
  const [files, setFiles] = useState([]);

  const {
    getRootProps,
    getInputProps,
    isDragActive,
    isDragAccept,
    isDragReject,
  } = useDropzone({
    accept: "image/*",
    onDrop: (newFiles) => {
      setFiles([...files, ...newFiles]);
    },
  });

  return (
    <>
      <div>
        <div {...getRootProps({ isDragActive, isDragAccept, isDragReject })}>
          <input {...getInputProps()} />
          <p>Drag 'n' drop some files here, or click to select files</p>
        </div>
      </div>
      {files.map(x => new FileReader.readAsDataURL(x))}
    </>
  );
}

export default ImagePicker;

*/
