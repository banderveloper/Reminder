import classes from "./Navbar.module.css";
import {Link} from "react-router-dom";

const Navbar = () => {

    return (
        <header>

            <nav className={classes.nav}>

                {/* Left side with logo */}
                <div className={`${classes.navGroup} ${classes.navLogoBlock}`}>
                    <span>Logo</span>
                    <span>Reminder</span>
                </div>

                {/* Right side with nav items*/}
                <div className={classes.navGroup}>
                    <Link to='/'>Prompts</Link>
                    <Link to='/auth'>Sign in</Link>
                </div>

            </nav>

        </header>
    );
};

export default Navbar;