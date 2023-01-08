/**
 * @param {{id: string, title: string, canSubmit: boolean, submitButtonName: string, children: any, onSubmit: () => {}}} props
 * @returns
 */
const Modal = (props) => {
    return (
        <div className="modal fade" id={props.id} tabIndex="-1" aria-labelledby={`${props.id}-label`} aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id={`${props.id}-label`}>{props.title}</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        {props.children}
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button disabled={!props.canSubmit} type="button" onClick={props.onSubmit} className="btn btn-primary">{props.submitButtonName}</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Modal;