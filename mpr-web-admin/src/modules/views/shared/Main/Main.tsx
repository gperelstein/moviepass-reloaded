import React, { memo } from 'react';

import { useStyles } from './styles';
import Sidebar from '../Sidebar';

export interface IMainProps {
  children: React.ReactNode;
}

const Main = ({ children }: IMainProps) => {
  const classes = useStyles();
  return (
    <>
      <>
        <Sidebar />
      </>      
      <main
        key="Main"
        className={classes.appContainer}
        onDragOver={/* istanbul ignore next line */ e => e.preventDefault()}
        onDrop={
          /* istanbul ignore next line */ e => {
            e.stopPropagation();
            e.preventDefault();
            return false;
          }
        }
      >
        <div className={classes.headerSpacer} />
        <section className={classes.sectionContainer}>
          {children}
        </section>
      </main>
    </>
  );
};

export default memo(Main);