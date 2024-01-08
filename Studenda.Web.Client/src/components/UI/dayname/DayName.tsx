import { FC } from 'react'
import classes from './DayName.module.css'

export enum ButtonVariant {
        outlined='outlined',
        primary='primary'
      }
      interface daynameProps {
              text1?: string;
              text2?: string;
              children?: React.ReactChild | React.ReactNode;
              onClick?: ()=> void;
      }

const DayName: FC<daynameProps> = ({
        text1, 
        text2,
        onClick,
        children

}) => {

    return (
        <div className={classes.buttonBox}>
                <button className={classes.dayButton} onClick={onClick}> {text1} {text2} </button>
                {children}
        </div>
    )
}

export default DayName