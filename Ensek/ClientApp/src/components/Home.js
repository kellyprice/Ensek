import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Alert, Table } from 'reactstrap';
import dayjs from 'dayjs';

function Home() {
    const [file, setFile] = useState();
    const [fileName, setFileName] = useState();
    const [error, setError] = useState();
    const [data, setData] = useState([]);
    const [successMsg, setSuccessMsg] = useState('');
    const [rejectedMsg, setRejectedMsg] = useState('');

    useEffect(() => {
        const noSuccessful = data.filter(x => !x.error).length;
        const noRejected = data.filter(x => x.error).length;
        setSuccessMsg(`${noSuccessful} record${noSuccessful !== 1 ? 's' : ''} successfully uploaded`);
        setRejectedMsg(`${noRejected} record${noRejected !== 1 ? 's' : ''} rejected`);
    }, [data]);

    const saveFile = (e) => {
        setFile(e.target.files[0]);
        setFileName(e.target.files[0].name);
    }

    const uploadFile = async () => {
        reset();
        const formData = new FormData();
        formData.append("formFile", file);
        formData.append("fileName", fileName);
        try {
            const res = await axios.post("http://localhost:44413/meter-reading-uploads", formData);
            setData(res.data);
        }
        catch (ex) {
            setError(ex.response.data);
            setData([]);
        }
    }

    const reset = () => {
        setError('');
    }

    return (
        <div className='w-75 mx-auto'>
            <div className="mb-20">
                <input type="file" onChange={saveFile} />
                <input type="button" value="upload" onClick={uploadFile} disabled={!file} />
            </div>
            <Alert className="mb-20" color="danger" hidden={!error}>{error}</Alert>
            <Table hidden={!data.length}>
                <thead>
                    <tr>
                        <th>Account Id</th>
                        <th>Meter Reading Date</th>
                        <th>Meter Reading</th>
                        <th>Errors</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colSpan={4}>
                            <div>{successMsg}</div>
                            <div>{rejectedMsg}</div>
                        </td>
                    </tr>
                    {
                        data.map((row, index) => {
                            return (
                                <tr key={index}>
                                    <td className="col-2">{row.accountId}</td>
                                    <td className="col-3">{dayjs(row.meterReadingDateTime).format('DD/MM/YYYY HH:mm A')}</td>
                                    <td className="col-2">{row.meterReadingValue}</td>
                                    <td className="col-4">{row.error}</td>
                                </tr>
                            )
                        })
                    }
                </tbody>
            </Table>
        </div>
    );
}

export default Home;