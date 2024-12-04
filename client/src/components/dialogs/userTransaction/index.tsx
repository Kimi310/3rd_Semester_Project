import { InputTypeEnum, TextInput } from "@components/inputs/textInput";
import QRCode from "react-qr-code";
import { useState } from "react";
import { BaseDialog, DialogSizeEnum, IBaseDialog } from "..";


export const UserTransactionDialog = (props: IBaseDialog) => { 
    const [transactionId, setTransactionId] = useState<string>("");
    const handleNewTransaction = () => {
        console.log(transactionId);
        props.close();
        setTransactionId("");
    };

    return (
        <>
            <BaseDialog isOpen={props.isOpen} close={props.close} dialogTitle="New Transaction" dialogSize={DialogSizeEnum.small} >
                <div className="flex flex-col justify-between gap-7 w-full h-full p-5">
                    <div className="flex flex-col justify-center items-center gap-7 w-full">
                        <div className="flex flex-col items-center justify-center">
                            <div className="text-lg font-bold">Scan the QR code to pay</div>
                            <div className="text-xs text-base-content">Scan the QR code, make the payment, and paste your transaction number below.</div>
                        </div>

                        <QRCode value="https://qr.mobilepay.dk/box/41ca2f58-d80d-45f9-82d5-aa8dea0029ae/pay-in" bgColor="#00000000" className="bg-white h-52 w-52 rounded-xl p-3" />

                        <TextInput parentClassName="flex flex-col gap-2 w-full" titleClassName="text-sm" inputType={InputTypeEnum.password} inputTitle="Transaction Number" setInput={setTransactionId} input={transactionId} />
                    </div>

                    
                    <button className="h-10 bg-primary text-primary-content rounded-xl w-full" onClick={handleNewTransaction}> Send Transaction </button>
                </div>
            </BaseDialog>
        </>
    )
}