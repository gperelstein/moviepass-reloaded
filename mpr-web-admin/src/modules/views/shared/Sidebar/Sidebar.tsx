import React, { memo, useMemo, useCallback } from 'react';

import Tabs from '@material-ui/core/Tabs';

import LinkTab from '../LinkTab';

import { MoviePassIcon, MoviesIcon, DashboardIcon, ROUTES } from '../../../../constants';
import { useStyles } from './styles';
import { getConditionalDefaultValue, isEmpty } from '../../../../utils/generalUtils';
import { useLocation } from 'react-router';

const Sidebar = () => {
  const styles = useStyles();
  var location = useLocation();
  const pathname = location.pathname.split('/')[1] || 'dashboard';
  const getTabMap = useCallback(() => {
    const tabMap = {
        dashboard: 0,
        movies: 1,
    };
    return tabMap;
  }, []);
  const indexTabMap = useMemo(() => getTabMap() as any, [getTabMap]);
  const currentTab = useMemo(() => getConditionalDefaultValue(isEmpty(indexTabMap[pathname]), false, indexTabMap[pathname]), [indexTabMap, pathname]);

  return (
    <div className={styles.container}>
      <span className={styles.logo}>
        <MoviePassIcon />
      </span>
      <Tabs
        orientation="vertical"
        variant="standard"
        value={currentTab}
        aria-label="Sidebar Tabs"
        data-testid="sidebar-wrapper"
        TabIndicatorProps={{
          style: {
            display: getConditionalDefaultValue(currentTab === false, 'none', 'flex'),
          },
        }}
      >
        <LinkTab
          selectedValue={indexTabMap[pathname] === indexTabMap.dashboard}
          label="Dashboard"
          data-testid="dashboard-link"
          to={ROUTES.DASHBOARD.path}
          icon={<DashboardIcon />}
          index={0}
        />
        <LinkTab
          selectedValue={indexTabMap[pathname] === indexTabMap.projects}
          label="Movies"
          to={ROUTES.MOVIES.path}
          icon={<MoviesIcon />}
          index={1}
        />
      </Tabs>
    </div>
  );
};

export default Sidebar;