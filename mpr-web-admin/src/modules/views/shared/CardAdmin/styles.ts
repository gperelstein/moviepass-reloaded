import { createStyles, makeStyles } from '@material-ui/core/styles';
import { STYLE } from '../../../../constants';
import { toREM } from '../../../../utils/generalUtils';

export const useStyles = makeStyles(theme =>
  createStyles({
    cardContainer: {
      width: 400,
      height: 250,
      background: '#FFF',
      boxSizing: 'border-box',
      border: '1px solid #E5E5E5',
      borderRadius: '5px',
      boxShadow: '0 2px 6px 0 rgba(0,0,0,0.03)',
      cursor: 'pointer'
    },
    titleContainer: {
      display: 'flex',
      flexDirection: 'row',
    },
    logoContainer: {
      paddingTop: '30px',
      marginLeft: '30px',
      marginRight: '30px',
      marginBottom: '25px',
      display: 'inline-block',
      '& svg': {
        position: 'relative',
        top: '1px',
        left: '2px',
      },
    },
    grayCircle: {
      borderRadius: '50%',
      width: '72px',
      height: '72px',
      background: '#F5F5F5',      
      justifyContent: 'center',
      alignItems: 'center',
    },
    title: {
      color: '#4C4C4C',
      fontWeight: 600,
      fontSize: '24px',
      marginTop: '51px',
    },
    bodyContainer: {
      marginLeft: '30px',

      '& p': {
        color: '#444444',
        fontSize: '15px',
        letterSpacing: 0,
      },
    },
    linkContanier: {
      marginLeft: '30px',
      marginTop: '27px',
    },
    logo: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: `${toREM(80)}`,
        width: `${toREM(80)}`,
        textAlign: 'center',
        marginBottom: '0'
      },
  })
);