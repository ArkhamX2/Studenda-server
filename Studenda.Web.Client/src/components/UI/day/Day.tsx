import { FC } from 'react'
import classes from './Day.module.css'
import Subject from '../subject/Subject'


const Day: FC = () => {

    return (
        <div className={classes.dayBox}>
                <Subject/>
                <Subject/>
                <Subject/>
        </div>
    )
}

export default Day