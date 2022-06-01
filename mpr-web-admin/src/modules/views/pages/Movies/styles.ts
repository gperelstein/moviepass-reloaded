import { createStyles, makeStyles } from '@material-ui/core/styles';
import { STYLE } from '../../../../constants';
import { toREM } from '../../../../utils/generalUtils';

export const useStyles = makeStyles(theme =>
  createStyles({
    container: {
      width: '100%',
      backgroundColor: STYLE.COLOR.QUINARY_LIGHTER,
      paddingTop: `${toREM(50)}`,
      paddingRight: `${toREM(40)}`,
      paddingBottom: `${toREM(46)}`,
      paddingLeft: `${toREM(40)}`,
    },
    cardsContainer: {
      marginTop: 38
    },
    adminName: {
      fontSize: `${toREM(STYLE.FONT.SIZE.HEADING_BIG)} !important`,
      marginBottom: '30px',
    },
    adminTitleWrapper: {
      width: '50%',
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
    },
    adminText: {
      color: STYLE.COLOR.SECONDARY_DARKER,
      fontSize: `${toREM(31)}`,
      letterSpacing: 0,
    },
    adminAccent: {
      fontWeight: 600,
    },
    adminTitleMarginRight: {
      marginRight: `${toREM(12)}`,
    },
    chipSize: {
      width: '70px',
      height: '20px !important',
      fontSize: '12px !important',
    },
    tableContainer: {
      '& .MuiTableCell-root': {
        height: 60,
      },
    },
    alignLeftStatus: {
      paddingRight: '42px !important',
    },
  })
);
