import { BaseDialog, DialogSizeEnum, IBaseDialog } from "..";
import { Button, Tab, TabGroup, TabList, TabPanel, TabPanels } from "@headlessui/react";
import { useLogout } from "@hooks/authentication/useLogout";
import { FiSettings, FiCreditCard, FiLayout, FiLogOut } from "react-icons/fi";
import classNames from 'classnames';

// TABS
import { AccountTabContent } from "./tabs/account";
import { BillingTabContent } from "./tabs/billing";
import { AppearanceTabContent } from "./tabs/appearance";

export const UserSettingsDialog = (props: IBaseDialog) => { 
    const logout = useLogout();

    const handleLogout = () => {
        props.close();
        logout();
    };

    return (
        <>
            <BaseDialog isOpen={props.isOpen} close={props.close} dialogTitle="Settings" dialogSize={DialogSizeEnum.mediumFixed} >
                <TabGroup className="flex flex-row w-full h-full overflow-hidden px-5">
                    <TabList className="flex flex-col min-w-40 max-w-40 border-r-0.05r border-base-content/50">
                        <Tab className={({selected}) => classNames("flex flex-row items-center gap-5 py-2.5 outline-none", selected ? 'border-r-0.25r !border-primary':'')}>
                            <div className="flex justify-center items-center"> <FiSettings className="opacity-60" size={20} /> </div>
                            <div className=""> Account </div>
                        </Tab>

                        <Tab className={({selected}) => classNames("flex flex-row items-center gap-5 py-2.5 outline-none", selected ? 'border-r-0.25r !border-primary':'')}>
                            <div className="flex justify-center items-center"> <FiCreditCard className="opacity-60" size={20} /> </div>
                            <div className=""> Billing </div>
                        </Tab>

                        <Tab className={({selected}) => classNames("flex flex-row items-center gap-5 py-2.5 outline-none", selected ? 'border-r-0.25r !border-primary':'')}>
                            <div className="flex justify-center items-center"> <FiLayout className="opacity-60" size={20} /> </div>
                            <div className=""> Appearance </div>
                        </Tab>
                        
                        <Button onClick={handleLogout} className="flex flex-row items-center gap-5 py-2.5 outline-none absolute bottom-0 mb-3 text-error"> 
                            <div className="flex justify-center items-center"> <FiLogOut className="opacity-60" size={20} /> </div>
                            <div className=""> Logout </div> 
                        </Button>
                    </TabList>

                    <TabPanels className="p-5 w-full h-full overflow-y-scroll">
                        <TabPanel> <AccountTabContent /> </TabPanel>
                        <TabPanel> <BillingTabContent /> </TabPanel>
                        <TabPanel> <AppearanceTabContent /> </TabPanel>
                    </TabPanels>
                </TabGroup>
            </BaseDialog>
        </>
    )
}