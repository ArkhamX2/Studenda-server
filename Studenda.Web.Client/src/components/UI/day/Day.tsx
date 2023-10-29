import { FC } from 'react'
import classes from './Day.module.css'


const Day: FC = () => {

    return (
        <div className="dayBox">
                <div className={classes.elementBox}>
                        <div className="timeContainer">
                                <p className="text">10.00</p>
                                <p className="text">11.00</p>
                        </div>
                        <div className="subjectContainer"><p className="text">Дело</p></div>
                        <div className="roomContainer"><p className="text">каб.123</p></div>
                </div>
                <div className="elementBox">
                        <div className="timeContainer">
                                <p className="text">10.00</p>
                                <p className="text">11.00</p>
                        </div>
                        <div className="subjectContainer"><p className="text">Дело</p></div>
                        <div className="roomContainer"><p className="text">каб.123</p></div>
                </div>
                <div className="elementBox">
                        <div className="timeContainer">
                                <p className="text">10.00</p>
                                <p className="text">11.00</p>
                        </div>
                        <div className="subjectContainer"><p className="text">Дело</p></div>
                        <div className="roomContainer"><p className="text">каб.123</p></div>
                </div>
        </div>
    )
}

export default Day