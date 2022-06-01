import { Tab, Tabs } from "@material-ui/core";
import { ChangeEvent, useState } from "react";
import PageTitle from "../../shared/PageTitle";
import TmdbGenresListContainer from "../TmdbGenresList";
import TmdbMoviesListContainer from "../TmdbMoviesList";

interface TabPanelProps {
    index: number;
    value: number;
    children: React.ReactNode;
  }
  
  function TabPanel(props: TabPanelProps) {
    const { value, index, children } = props;
  
    return (
        <>
            {(value === index) ?
                <div>            
                    {children}
                </div> : null
            }
        </>
        
        
    );
  }
  
  function a11yProps(index: number) {
    return {
      id: `simple-tab-${index}`,
      'aria-controls': `simple-tabpanel-${index}`,
    };
  }

const TheMovieDb = () => {
    const [value, setValue] = useState(0);

    const handleChange = (event: ChangeEvent<{}>, newValue: number) => {
        setValue(newValue);
    };

    return (
        <>            
            <PageTitle
                title="The Movie Db"
            />
            <Tabs value={value} onChange={(event, value) => handleChange(event, value)} aria-label="nav tabs example">
                <Tab label="Movies" />
                <Tab label="Genres" />
            </Tabs>            
            <TabPanel index={0} value={value}>
                <TmdbMoviesListContainer />
            </TabPanel>
            <TabPanel index={1} value={value}>
                <TmdbGenresListContainer />
            </TabPanel>
        </>
    );
}

export default TheMovieDb;