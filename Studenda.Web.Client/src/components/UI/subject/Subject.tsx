import { FC } from 'react'
import classes from './Subject.module.css'


const Subject: FC = () => {

    return (
                <div className={classes.elementBox}>
                        <div className={classes.timeContainer}>
                                <p className={classes.text}>10.00</p>
                                <p className={classes.text}>11.00</p>
                        </div>
                        <div className={classes.subjectContainer}><p className={classes.text}>Дело1</p></div>
                        <div className={classes.roomContainer}><p className={classes.text}>каб.123</p></div>
                </div>
    )
}

export default Subject