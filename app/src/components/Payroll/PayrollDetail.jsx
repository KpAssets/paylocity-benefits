import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Accordion from 'react-bootstrap/Accordion';
import PayrollCategoryDetail from "./PayrollCategoryDetail";
import { currencyFormat } from '../../Constants';

const PayrollDetail = (props) => {
    return (
        <Container>
            <Row>
                <Col>
                    <Accordion>
                        <PayrollCategoryDetail title="Earnings" items={props.payroll.earnings} />
                        <PayrollCategoryDetail title="Deductions" items={props.payroll.deductions} />
                    </Accordion>
                </Col>
            </Row>
            <Row>
                <Col sm={7} />
                <Col sm={5}>
                    <p>Net Pay: {currencyFormat(props.payroll?.netPay || 0)}</p>
                </Col>
            </Row>
        </Container>
    );
};

export default PayrollDetail;